using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Order : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public decimal TotalPrice { get; set; }

        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<OrderItem> OrderItem { get; set; }
    }

    public class OrderItem
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid CourseId { set; get; }
        public Courses Course { get; set; }
    }
}
