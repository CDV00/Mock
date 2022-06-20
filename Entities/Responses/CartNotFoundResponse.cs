using System;

namespace Entities.Responses
{
    public class CourseNotFoundResponse : ApiNotFoundResponse
    {
        public CourseNotFoundResponse(Guid id)
        : base($"course with id: {id} is not found in db.")
        {
        }
    }
}
