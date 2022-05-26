using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class CourseReviewRequest
    {
        public Guid id { get; set; }
        public Guid EnrollmentId { get; set; }
        public string Content { get; set; }
        [Required]
        public int Rating { get; set; }
        //[Required]
        //public Guid EnrollmentId { get; set; }
    }
    public class CourseReviewUpdateRequest : CourseReviewRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}
