using System;

namespace Course.DAL.Models
{
    public class Order : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal Price { get; set; }

        public AppUser User { get; set; }
        public Courses Course { get; set; }

        public Guid? DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}
