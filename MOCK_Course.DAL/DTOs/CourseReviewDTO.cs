using System;

namespace Course.BLL.Responses
{
    public class CourseReviewDTO
    {
        public Guid Id;
        public string Content { get; set; }
        public float Rating { get; set; }
        public DateTime CreateAt { get; set; }
        public EnrollmentDTO Enrollment { get; set; }
    }
}
