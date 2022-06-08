using System;

namespace Course.DAL.Models
{
    public class CourseReview : BaseEntity<Guid>
    {
        public string Content { get; set; }
        public float Rating { get; set; }

        public Guid EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }

        public CourseReview()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
