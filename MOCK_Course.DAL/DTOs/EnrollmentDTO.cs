using Course.BLL.DTO;

namespace Course.BLL.Responses
{
    public class EnrollmentDTO
    {
        public UserDTO User { get; set; }
        public CourseDTO Courses { get; set; }
    }
}
