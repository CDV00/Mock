using System;

namespace Entities.Responses
{
    public class ExistSaveCourseResponse : ApiUnprocessableResponse
    {
        public ExistSaveCourseResponse(Guid courseId, Guid UserId) : base(message: $"Already saved this course id: {courseId}", null) { }
    }
}
