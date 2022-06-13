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

namespace Course.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICousesRepository _cousesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICousesRepository cousesRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cousesRepository = cousesRepository;
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
            var cartIDs = orderRequest.Carts.Select(x => x.Id);
            var orderId = Guid.NewGuid();
            var order = _mapper.Map<Order>(orderRequest);
            decimal totalPrice = 0;
            var instructors = new List<Guid>();

            //Create Order
            foreach (var cart in orderRequest.Carts)
            {
                totalPrice += cart.Course.Price - cart.Course.Discount;
                instructors.Add(cart.Course.AuthorId);
            }
            order.TotalPrice = totalPrice;
            order.Id = orderId;

            await _orderRepository.CreateAsync(order);


            //Create OrderItems
            foreach (var cartId in cartIDs)
            {
                var cart = await _shopingCartRepository.FindByCondition(x => x.IsActive && x.Id.Equals(cartId)).FirstOrDefaultAsync();
                var orderItem = new OrderItemDTO
                {
                    OrderId = orderId,
                    CourseId = cart.CourseId
                };


                await _orderItemRepository.CreateAsync(_mapper.Map<OrderItem>(orderItem));


                //Create Enrollment
                var enrollment = new EnrollmentCreateDTO()
                {
                    Id = Guid.NewGuid(),
                    CourseId = cart.CourseId,
                    UserId = orderRequest.UserId.GetValueOrDefault()
                };


                await _enrollmentRepository.CreateAsync(_mapper.Map<Enrollment>(enrollment));

                //Remove Cart
                _shopingCartRepository.Remove(cart);
            }
            //payment stripe
            if (orderRequest.PaymentType == PaymentType.Stripe)
            {
                var resultPayment = await _paymentService.PayAsync(orderRequest.Payment);
                if (!resultPayment.Equals("Success"))
                {
                    return new OrderResponse
                    {
                        IsSuccess = false,
                        Message = resultPayment
                    };
                }

            }
            //Payment by Balance of User
            else
            {
                var user = await _userManager.FindByIdAsync(orderRequest.UserId.ToString());
                user.Balance -= totalPrice;
                if (user.Balance < 0)
                {
                    return new OrderResponse
                    {
                        IsSuccess = false,
                        Message = "you don't have enough money to payment"
                    };
                }
                await _userManager.UpdateAsync(user);

            }

            //Deposit Instuctor
            foreach (var instructorId in instructors)
            {
                var instructor = await _userManager.FindByIdAsync(instructorId.ToString());
                instructor.Balance += totalPrice;
                await _userManager.UpdateAsync(instructor);
            }

            //Save
            await _unitOfWork.SaveChangesAsync();

            return new OrderResponse
            {
                IsSuccess = true,
                Data = _mapper.Map<OrderDTO>(order)
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
