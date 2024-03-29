﻿using System.Collections.Generic;

namespace Course.BLL.DTO
{
    public class Response<T> : BaseResponse
    {
        public T data { get; set; }

        public Response(Entities.DTOs.ListSaleAnalysisResponse listSaleAnalysisResponse) { }
        public Response(bool isSuccess) : base(isSuccess)
        {
            this.data = data;
        }
        public Response(bool isSuccess, T data) : base(isSuccess)
        {
            this.data = data;
        }
        public Response(bool isSuccess, string message, string error) : base(isSuccess, error, message)
        {
        }
    }
    public class Responses<T> : BaseResponse
    {
        public IEnumerable<T> Data { get; set; }
        public Responses(bool isSuccess, IEnumerable<T> data) : base(isSuccess)
        {
            Data = data;
        }
        public Responses(bool isSuccess, string message, string error) : base(isSuccess, error, message)
        {
        }
    }
}
