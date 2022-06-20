using System;

namespace Entities.Responses
{
    public class DiscountNotFound : ApiNotFoundResponse
    {
        public DiscountNotFound(Guid id)
        : base($"Discount with id: {id} is not found in db.")
        {
        }
    }
}
