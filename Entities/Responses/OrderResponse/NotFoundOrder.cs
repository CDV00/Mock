using System;

namespace Entities.Responses
{
    public class NotFoundOrder : ApiNotFoundResponse
    {
        public NotFoundOrder(Guid courseId)
        : base($"order with course id: {courseId} is not found in db.")
        {
        }
    }
}
