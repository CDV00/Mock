using System;

namespace Entities.Responses
{
    public class InvalidEnrollCoursePrice : ApiUnprocessableResponse
    {
        public InvalidEnrollCoursePrice(Guid id) : base(message: $"You aren't order course with id: {id}", null) { }
    }
}
