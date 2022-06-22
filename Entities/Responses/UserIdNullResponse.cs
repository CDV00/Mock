using System;

namespace Entities.Responses
{
    public class UserIdNullResponse : ApiBadRequestResponse
    {
        public UserIdNullResponse()
        : base($"You must pass userId or authentication")
        {
        }
    }
}
