using System;

namespace Entities.Responses
{
    public class InvalidPriceCartResponse : ApiUnprocessableResponse
    {
        public InvalidPriceCartResponse(Guid id) : base(message: $"Can't add course with id:{id} because it free", null) { }
    }
}
