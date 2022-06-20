using System;

namespace Entities.Responses
{
    public class CartNotFoundResponse : ApiNotFoundResponse
    {
        public CartNotFoundResponse(Guid id)
        : base($"cart with id: {id} is not found in db.")
        {
        }
    }
}
