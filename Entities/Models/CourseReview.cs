using System;
using System.ComponentModel.DataAnnotations;

namespace Course.DAL.Models
{
    public class CourseReview : BaseEntity<Guid>
    {
        [MaxLength(500)]
        public string Content { get; set; }
        [Range(1, 5)]
        public float Rating { get; set; }

        public Guid EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }

        public CourseReview()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
