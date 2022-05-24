using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class EnrollmentRequest
    {
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
    public class EnrollmentUpdateRequest : EnrollmentRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}
