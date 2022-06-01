using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
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
        public async Task<Responses<OrderDTO>> GetAll(Guid UserId)
        {
            try
            {
                var result = await _orderRepository.GetAll().Where(o => o.UserId == UserId).ToListAsync();

                return new Responses<OrderDTO>(true, _mapper.Map<List<OrderDTO>>(result));
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
                var order = _mapper.Map<Order>(orderRequest);
                order.UserId = userId;

                var course = await _cousesRepository.GetByIdAsync(order.CourseId);
                order.Price = course.Price;

                await _orderRepository.CreateAsync(order);
                await _unitOfWork.SaveChangesAsync();
                return new Response<OrderDTO>(
                    true,
                    _mapper.Map<OrderDTO>(order)
                );
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
                _orderRepository.Remove(order);

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
