using System;

namespace Entities.Responses
{
    public class DuplicateSaveCourseResponse : ApiUnprocessableResponse
    {
        public DuplicateSaveCourseResponse(Guid courseId) : base(message: $"You already saved this course id: {courseId}", null) { }
    }
}
