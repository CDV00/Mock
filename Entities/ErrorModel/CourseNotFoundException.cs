using System;

namespace CourseAPI.ErrorModel
{
    public class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(Guid course)
        : base($"The course with id: {course} doesn't exist in the database.")
        {
        }
    }

}
