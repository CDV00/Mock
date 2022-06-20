using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class CourseReviewRequest
    {
        public Guid EnrollmentId { get; set; }
        public string Content { get; set; }
        [Required]
        public float Rating { get; set; }
    }
    public class CourseReviewUpdateRequest
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public float Rating { get; set; }
    }
}
