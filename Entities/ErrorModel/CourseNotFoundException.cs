using System;
using System;

namespace CourseAPI.ErrorModel
{
    public class CourseNotFoundException : NotFoundException
    {
        public CourseNotFoundException(Guid course)
        : base($"The course with id: {course} doesn't exist in the database.")
        {
        }
    }

}
