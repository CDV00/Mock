using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Requests
{
    public class OrderRequest
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal Price { get; set; }
    }
    public class OrderUpdateRequest : OrderRequest
    {
        public Guid Id { get; set; }
    }
}