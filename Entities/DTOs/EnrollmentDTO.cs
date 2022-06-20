using Course.BLL.DTO;
using System;

namespace Course.BLL.Responses
{
    public class EnrollmentDTO
    {
        public Guid Id { get; set; }
        public UserDTO User { get; set; }
        public CourseDTO Courses { get; set; }
    }
}
