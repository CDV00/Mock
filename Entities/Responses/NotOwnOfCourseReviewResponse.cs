using System;

namespace Entities.Responses
{
    public class NotOwnOfCourseReviewResponse : ApiUnprocessableResponse
    {
        public NotOwnOfCourseReviewResponse(Guid id) : base(message: $"You aren't the owner of the Review with id:{id}", null) { }
    }
}
