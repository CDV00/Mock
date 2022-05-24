﻿using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Responsesnamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IOrderService
    {
        Task<Responses<OrderResponse>> GetAll();
        Task<Response<OrderResponse>> GetById(Guid id);
        Task<Response<OrderResponse>> Add(OrderRequest orderRequest);
        Task<Response<OrderResponse>> Update(OrderUpdateRequest orderUpdateRequest);
        Task<Responsesnamespace.BaseResponse> Delete(Guid id);

    }
}
