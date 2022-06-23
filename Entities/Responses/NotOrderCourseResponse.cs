using System;

namespace Entities.Responses
{
    public class NotOrderCourseResponse : ApiUnprocessableResponse
    {
        public NotOrderCourseResponse(Guid id) : base(message: $"You aren't order course with id:{id}", null) { }
    }
}
