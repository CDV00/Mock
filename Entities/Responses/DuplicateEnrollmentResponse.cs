using System;

namespace Entities.Responses
{
    public class DuplicateEnrollmentResponse : ApiUnprocessableResponse
    {
        public DuplicateEnrollmentResponse(Guid courseId) : base(message: $"You already enroll this course id: {courseId}", null) { }
    }
}
