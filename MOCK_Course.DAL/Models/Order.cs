using System;

namespace Course.DAL.Models
{
    public class Order : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public float Price { get; set; }

        public AppUser User { get; set; }
        public Courses Course { get; set; }
    }
}
