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
using Repository.Repositories;
using MOCK_Course.BLL.Services.Implementations;
using Microsoft.AspNetCore.Identity;

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

        public async Task<Response<int>> GetTotal(Guid courseId)
        {
            try
            {

                var count = await _orderRepository.BuildQuery()
                                                  .FilterByCourseId(courseId)
                                                  .CountAsync();

                return new Response<int>(
                    true,
                    _mapper.Map<int>(count)
                );
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }
        public async Task<Responses<OrderDTO>> GetAll(Guid UserId)
        {
            try
            {
                var result = await _orderRepository.BuildQuery()
                                                   .FilterByUserId(UserId)
                                                   .ToListAsync(o => _mapper.Map<OrderDTO>(o));
                return new Responses<OrderDTO>(true, result);
            }
            catch (Exception ex)
            {
                return new Responses<OrderDTO>(false, ex.Message, null);
            }
        }

        /// <summary>
        /// Add Order
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        public async Task<Response<OrderDTO>> Add(Guid userId, OrderRequest orderRequest)
        {
            try
            {
                var courses = await _cousesRepository.BuildQuery()
                                                     .FilterByIds(orderRequest.CourseIds)
                                                     .IncludeDiscount()
                                                     .ToListAsync(c => c);

                if (courses == null || courses.Count <= 0)
                    return new Response<OrderDTO>(false, "Don't have any course to payment", null);

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

                    await CreateEnroll(userId, course);
                    await UpdateBalanceOfInstrucctor(course, coursePrice);
                    await RemoveCart(userId, course);
                }

                order.OrderItem = orderItems;
                order.TotalPrice = totalPrice;
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
                                return new Response<OrderDTO>(false, resultPayment.StatusCode, resultPayment.Message);
                            }

                            break;
                        }

                    default:
                        {
                            var user = await _userManager.FindByIdAsync(userId.ToString());
                            user.Balance -= totalPrice;
                            if (user.Balance < 0)
                            {
                                return new Response<OrderDTO>
                                {
                                    IsSuccess = false,
                                    StatusCode = "You don't have enough money to payment"
                                };
                            }
                            await _userManager.UpdateAsync(user);
                            break;
                        }
                }

                await _unitOfWork.SaveChangesAsync();

                return new Response<OrderDTO>
                {
                    IsSuccess = true,
                    data = _mapper.Map<OrderDTO>(order)
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderDTO>(false, ex.Message, null);
            }
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

        /// <summary>
        /// delete order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseResponse> Delete(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order is null)
                {
                    return new BaseResponse(false, null, "can't order lesson");
                }
                _orderRepository.Remove(order, false);

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);

            }
        }

            public async Task<Response<OrderItemDTO>> IsPurchased(Guid userId, Guid courseId)
            {
                try
                {
                    var purchase = await _orderRepository.BuildQuery()
                                                           .FilterByUserId(userId)
                                                           .FilterByCourseId(courseId)
                                                           .AsSelectorAsync(e => _mapper.Map<OrderItemDTO>(e));

                if (purchase == null)
                {
                    return new Response<OrderItemDTO>(true, null);
                }

                return new Response<OrderItemDTO>(true, purchase);
            }
                catch (Exception ex)
                {
                    return new Response<OrderItemDTO>(false, ex.Message, null);
                }
            }
        }
}
