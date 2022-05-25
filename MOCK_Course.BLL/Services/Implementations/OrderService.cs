﻿using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Responsesnamespace;
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
        /// <summary>
        /// Get All Order
        /// </summary>
        /// <returns></returns>
        //public async Task<Responses<OrderResponse>> GetAll()
        //{
        //    try
        //    {
        //        var result = await _orderRepository.GetAll().Where(c => c.IsDeleted == false).ToListAsync();
        //        return new Responses<OrderResponse>(true, _mapper.Map<IEnumerable<OrderResponse>>(result));

        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<OrderResponse>(false, ex.Message, null);
        //    }

        //}
        /// <summary>
        /// get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Responses<OrderResponse>> GetAll(Guid UserId)
        {
            try
            {
                var result = await _orderRepository.GetAll().Where(o=>o.UserId == UserId).ToListAsync();

                return new Responses<OrderResponse>(true, _mapper.Map<List<OrderResponse>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<OrderResponse>(false, ex.Message, null);
            }
        }

        /// <summary>
        /// Add Order
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        public async Task<Response<OrderResponse>> Add(OrderRequest orderRequest)
        {
            try
            {
                var order = _mapper.Map<Order>(orderRequest);

                var course = await _cousesRepository.GetByIdAsync(order.CourseId);
                order.Price = course.Price;

                await _orderRepository.CreateAsync(order);
                await _unitOfWork.SaveChangesAsync();
                return new Response<OrderResponse>(
                    true,
                    _mapper.Map<OrderResponse>(order)
                );
            }
            catch (Exception ex)
            {
                return new Response<OrderResponse>(false, ex.Message, null);
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
                _orderRepository.Remove(order);

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false,ex.Message,null);
            }
        }


        ///// <summary>
        ///// update order
        ///// </summary>
        ///// <param name="orderUpdateRequest"></param>
        ///// <returns></returns>
        //public async Task<Response<OrderResponse>> Update(OrderUpdateRequest orderUpdateRequest)
        //{
        //    try
        //    {
        //        var order = _mapper.Map<Order>(orderUpdateRequest);

        //        _orderRepository.Update(order);
        //        await _unitOfWork.SaveChangesAsync();
        //        return new Response<OrderResponse>(
        //            true,
        //            _mapper.Map<OrderResponse>(order)
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<OrderResponse>(false, ex.Message, null);
        //    }
        //}
    }
}
