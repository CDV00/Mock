using System;

namespace Entities.Responses
{
    public class OrderNotFoundResponse : ApiNotFoundResponse
    {
        public OrderNotFoundResponse(Guid id)
        : base($"Order with id: {id} is not found in db.")
        {
        }
    }
}
