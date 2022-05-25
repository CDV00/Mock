using System;

namespace Course.BLL.Requests
{
    public class OrderRequest
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
    }
    public class OrderUpdateRequest : OrderRequest
    {
    }
}