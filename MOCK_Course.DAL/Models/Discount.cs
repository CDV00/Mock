using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Discount : BaseEntity<Guid>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }

        public ICollection<Courses> Courses { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
