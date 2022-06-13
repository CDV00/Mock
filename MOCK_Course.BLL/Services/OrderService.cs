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
        private readonly ICousesRepository _cousesRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        private readonly UserManager<AppUser> _userManager;

        public OrderService(IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICousesRepository cousesRepository,
            IShoppingCartRepository shoppingCartRepository,
            IOrderItemRepository orderItemRepository,
            IPaymentService paymentService,
            IEnrollmentRepository enrollmentRepository,
            UserManager<AppUser> userManager)
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
                //var cartIDs = orderRequest.Carts.Select(x => x.Id);
                var carts = await _shoppingCartRepository.BuildQuery()
                                                         .FilterByIds(orderRequest.CartIds)
                                                         .IncludeCourse()
                                                         .IncludeDiscount()
                                                         .ToListAsync(c => c);

                if (carts == null || carts.Count <= 0)
                    return new Response<OrderDTO>(false, "don't have any course in cart", null);

                var orderId = Guid.NewGuid();
                var order = _mapper.Map<Order>(orderRequest);
                order.UserId = userId;
                order.Id = orderId;
                decimal totalPrice = 0;
                //var instructors = new List<Guid>();

                var orderItems = new List<OrderItem>();
                //Create Order
                foreach (var cart in carts)
                {
                    orderItems.Add(new OrderItem { CourseId = cart.CourseId, OrderId = orderId });

                    decimal discount = 0;
                    if (cart.Course.Discounts.Any())
                        discount = cart.Course.Discounts.FirstOrDefault().DiscountPercent;

                    var coursePrice = cart.Course.Price * (100 - discount) / 100;
                    totalPrice += coursePrice;
                    //instructors.Add(cart.Course.User.Id);

                    var enrollment = new Enrollment()
                    {
                        CourseId = cart.CourseId,
                        UserId = userId
                    };
                    await _enrollmentRepository.CreateAsync(enrollment);

                    var instructor = await _userManager.FindByIdAsync(cart.Course.UserId.ToString());
                    instructor.Balance += coursePrice;
                    await _userManager.UpdateAsync(instructor);

                    _shoppingCartRepository.Remove(cart, true);
                }
                order.OrderItem = orderItems;
                order.TotalPrice = totalPrice;
                orderRequest.Payment.value = (int)totalPrice;

                await _orderRepository.CreateAsync(order);

                //payment stripe
                if (orderRequest.PaymentType == PaymentType.Stripe)
                {
                    var resultPayment = await _paymentService.PayAsync(orderRequest.Payment);
                    if (!resultPayment.IsSuccess)
                    {
                        return new Response<OrderDTO>(false, resultPayment.Message, resultPayment.Error);
                    }
                }
                //Payment by Balance of User
                else
                {
                    var user = await _userManager.FindByIdAsync(userId.ToString());
                    user.Balance -= totalPrice;
                    if (user.Balance < 0)
                    {
                        return new Response<OrderDTO>
                        {
                            IsSuccess = false,
                            Message = "you don't have enough money to payment"
                        };
                    }
                    await _userManager.UpdateAsync(user);
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
    }
}
