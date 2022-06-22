using System;

namespace Entities.Responses
{
    public class EnrollmentNotFoundResponse : ApiNotFoundResponse
    {
        public EnrollmentNotFoundResponse(Guid id)
        : base($"Enrollment with id: {id} is not found in db.")
        {
        }
    }
}
