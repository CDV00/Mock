using Course.BLL.Responses;
using System;

namespace Course.BLL.DTO
{
    public class SavedCoursesDTO
    {
        public UserDTO User { get; set; }
        public CourseDTO Course { get; set; }

    }
}
