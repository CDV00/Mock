using Course.BLL.Responses;
using System;

namespace Course.BLL.DTO
{
    public class SavedCoursesDTO
    {
        public Guid Id { get; set; }
        //public UserDTO User { get; set; }
        public CourseDTO Course { get; set; }

    }
}
