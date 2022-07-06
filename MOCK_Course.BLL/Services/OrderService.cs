using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.DAL.Models;
using System;
using System.Threading.Tasks;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Entities.Responses;
using Repository.Repositories.Abstraction;
using Course.BLL.Share.RequestFeatures;
using Entities.ParameterRequest;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Course.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICourseRepository _cousesRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        private readonly UserManager<AppUser> _userManager;

        public OrderService(IOrderRepository orderRepository,
                            IMapper mapper,
                            IUnitOfWork unitOfWork,
                            ICourseRepository cousesRepository,
                            IShoppingCartRepository shoppingCartRepository,
                            IOrderItemRepository orderItemRepository,
                            IPaymentService paymentService,
                            IEnrollmentRepository enrollmentRepository,
                            UserManager<AppUser> userManager,
                            IDiscountRepository discountRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cousesRepository = cousesRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _orderItemRepository = orderItemRepository;
            _paymentService = paymentService;
            _discountRepository = discountRepository;
        }

        public async Task<ApiBaseResponse> GetTotal(Guid courseId)
        {
            var count = await _orderRepository.BuildQuery()
                                              .FilterByCourseId(courseId)
                                              .CountAsync();

            return new ApiOkResponse<int>(_mapper.Map<int>(count));
        }
        public async Task<ApiBaseResponse> GetAll(Guid UserId)
        {
            var result = await _orderRepository.BuildQuery()
                                               .FilterByUserId(UserId)
                                               .ToListAsync(o => _mapper.Map<OrderDTO>(o));

            return new ApiOkResponse<List<OrderDTO>>(result);
        }

        /// <summary>
        /// Add Order
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        public async Task<ApiBaseResponse> Add(Guid userId, OrderRequest orderRequest)
        {
            var courses = await _cousesRepository.BuildQuery()
                                                 .FilterByIds(orderRequest.CourseIds)
                                                 .IncludeDiscount()
                                                 .ToListAsync(c => c);

            if (courses == null || courses.Count <= 0)
                return new CourseNotFoundToPayResponse();

            var orderId = Guid.NewGuid();
            var order = _mapper.Map<Order>(orderRequest);
            order.UserId = userId;
            order.Id = orderId;
            decimal totalPrice = 0;

            var orderItems = new List<OrderItem>();
            //Create Order
            foreach (var course in courses)
            {
                var orderItem = new OrderItem { CourseId = course.Id, OrderId = orderId };
                orderItems.Add(orderItem);

                decimal discountPercent = await GetDiscountPercent(course, orderItem);

                var coursePrice = course.Price * (100 - discountPercent) / 100;
                totalPrice += coursePrice;

                await UpdateBalanceOfInstrucctor(course, coursePrice);
                await RemoveCart(userId, course);
            }

            order.OrderItem = orderItems;
            order.TotalPrice = totalPrice;
            order.CreatedAt = DateTime.UtcNow;

            orderRequest.Payment.value = (int)totalPrice;

            await _orderRepository.CreateAsync(order);

            //payment stripe
            switch (orderRequest.PaymentType)
            {
                case PaymentType.Stripe:
                    {
                        var resultPayment = await _paymentService.PayAsync(orderRequest.Payment);
                        if (!resultPayment.IsSuccess)
                        {
                            return new PaymentFailResponse(resultPayment.Message, int.Parse(resultPayment.StatusCode));
                        }

                        break;
                    }

                default:
                    {
                        var user = await _userManager.FindByIdAsync(userId.ToString());
                        user.Balance -= totalPrice;
                        if (user.Balance < 0)
                        {
                            return new DontEnoughtMoney(user.Balance, totalPrice);
                        }
                        await _userManager.UpdateAsync(user);
                        break;
                    }
            }

            await _unitOfWork.SaveChangesAsync();

            return new ApiOkResponse<OrderDTO>(_mapper.Map<OrderDTO>(order));
        }

        private async Task UpdateBalanceOfInstrucctor(Courses course, decimal coursePrice)
        {
            var instructor = await _userManager.FindByIdAsync(course.UserId.ToString());
            instructor.Balance += coursePrice;
            //await _userManager.UpdateAsync(instructor);
        }

        private async Task CreateEnroll(Guid userId, Courses course)
        {
            var enrollment = new Enrollment()
            {
                CourseId = course.Id,
                UserId = userId
            };

            await _enrollmentRepository.CreateAsync(enrollment);
        }

        private async Task<decimal> GetDiscountPercent(Courses course, OrderItem orderItem)
        {
            decimal discountPercent = 0;
            if (await _discountRepository.BuildQuery()
                                         .FilterByCourseId(course.Id)
                                         .CheckDuringDate()
                                         .AnyAsync())
            {
                var discount = await _discountRepository.BuildQuery()
                                                        .FilterByCourseId(course.Id)
                                                        .CheckDuringDate()
                                                        .IncludeOrderItem()
                                                        .AsSelectorAsync(d => d);
                discountPercent = discount.DiscountPercent;


                discount.OrderItems.Add(orderItem);
            }

            return discountPercent;
        }

        private async Task RemoveCart(Guid userId, Courses course)
        {
            var cart = await _shoppingCartRepository.BuildQuery()
                                                    .FilterByCourseId(course.Id)
                                                    .FilterByUserId(userId)
                                                    .AsSelectorAsync(c => c);
            if (cart != null)
                _shoppingCartRepository.Remove(cart, true);
        }


        /// <summary>
        /// delete order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<ApiBaseResponse> IsPurchased(Guid userId, Guid courseId)
        {
            var purchase = await _orderRepository.BuildQuery()
                                                 .FilterByUserId(userId)
                                                 .FilterByCourseId(courseId)
                                                 .AsSelectorAsync(e => _mapper.Map<OrderItemDTO>(e));

            if (purchase == null)
            {
                return new NotFoundOrder(courseId);
            }

            return new ApiBaseResponse(true);
        }
        public async Task<ApiBaseResponse> GetDetail(Guid id)
        {
            var order = await _orderRepository.GetDetailOrder(id);
            if (order is null)
                return new OrderNotFoundResponse(id);

            return new ApiOkResponse<OrderDTO>(order);
        }
        //
        public async Task<ApiBaseResponse> GetEarning(OrderParameters orderParameters, Guid userId)
        {
            var earning = await _orderRepository.GetEarningAsync(orderParameters, userId);
            return new ApiOkResponse<PagedList<EarningDTO>>(earning);
        }
        //
        public async Task<ApiBaseResponse> GetStatements(OrderParameters orderParameters, Guid userId)
        {
            var statements = await _orderRepository.GetStatementsAsync(orderParameters, userId);
            return new ApiOkResponse<PagedList<StatementsDTO>>(statements);
        }
        public async Task<ApiOkResponse<ListAnalysisOrderResponse>> SumMoneyOrderByMonth(Guid userId)
        {
            var filterdata = await _orderRepository.FindByCondition(j => j.Id == j.OrderItem.Where(o => o.Course.UserId == userId)
            .Select(o => o.OrderId)
            .FirstOrDefault())
            .ToListAsync();

            var yearAndMonthRange = filterdata.Select(m => m.CreatedAt.Year).Distinct().OrderBy(t => t).Select(m => Enumerable.Range(1, 12).Select(t => new { Year = m, Month = t })).ToList();

            var result = yearAndMonthRange.Select(m => m.GroupJoin(filterdata, key => key, f => new { Year = f.CreatedAt.Year, Month = f.CreatedAt.Month }, (key, f) => new OrderAnalysisDisplayDTO { Year = key.Year, Month = key.Month, Amount = f.Sum(o=>o.TotalPrice) }).OrderBy(d => d.Year).ThenBy(d => d.Month).ToList()).ToList();

            var totalCount = filterdata.Sum(o=>o.TotalPrice);

            return new ApiOkResponse<ListAnalysisOrderResponse>(new ListAnalysisOrderResponse
            {

                Data = result,
                Total = totalCount
            }
            );
        }
    }
}
