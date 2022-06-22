using System;

namespace Entities.Responses
{
    public class EnrollmentNotForUserFoundResponse : ApiNotFoundResponse
    {
        public EnrollmentNotForUserFoundResponse(Guid userId)
        : base($"User with id: {userId} don't have any enrollment in db.")
        {
        }
    }
}
