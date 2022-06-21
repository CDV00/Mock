using System;

namespace Entities.Responses
{
    public class DuplicateSaveCourseResponse : ApiUnprocessableResponse
    {
        public DuplicateSaveCourseResponse(Guid courseId, Guid UserId) : base(message: $"Already saved this course id: {courseId}", null) { }
    }
}
