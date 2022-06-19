using System;

namespace CourseAPI.ErrorModel
{
    public class DiscountNotFoundException : NotFoundException
    {
        public DiscountNotFoundException(Guid course)
         : base($"The discount with id: {course} doesn't exist in the database.")
        {
        }
    }

}
