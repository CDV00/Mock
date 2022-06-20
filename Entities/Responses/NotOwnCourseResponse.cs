using System;

namespace Entities.Responses
{
    public class NotOwnOfCourseResponse : ApiUnprocessableResponse
    {
        public NotOwnOfCourseResponse(Guid id) : base(message: $"You aren't the owner of the course with id:{id}", null) { }
    }
}
