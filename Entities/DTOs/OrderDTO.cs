using Course.BLL.DTO;
using System;
using System.Collections.Generic;

namespace Course.BLL.Responses
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        //public Guid UserId { get; set; }
        public UserDTO User { get; set; }
        public List<OrderItemDTO> OrderItem { get; set; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderItemDTO
    {
        //public Guid OrderId { get; set; }
        //public Guid CourseId { set; get; }
        public CourseDTO Course { get; set; }
        public DiscountDTO_ Discount { get; set; }
    }
    public class EarningDTO
    {
        public int Count { get; set; }
        public Decimal Earning { get; set; }
        public DateTime Date { get; set; }
        
    }
}