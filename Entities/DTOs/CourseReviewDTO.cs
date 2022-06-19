using System;

namespace Course.BLL.Responses
{
    public class CourseReviewDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public float Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public EnrollmentDTO Enrollment { get; set; }
    }
}
