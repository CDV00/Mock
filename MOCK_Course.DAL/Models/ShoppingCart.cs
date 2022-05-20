using System;

namespace Course.DAL.Models
{
    public class ShoppingCart : BaseEntity<Guid>
    {
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }

        public AppUser User { get; set; }
        public Courses Course { get; set; }
    }
}
