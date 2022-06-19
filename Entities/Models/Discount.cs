using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Discount : BaseEntity<Guid>
    {
        public Guid CourseId { get; set; }
        public Courses Course { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
