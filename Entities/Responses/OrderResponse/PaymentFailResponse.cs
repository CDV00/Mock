using System;

namespace Entities.Responses
{
    public class PaymentFailResponse : ApiUnprocessableResponse
    {
        public PaymentFailResponse(string message, int? statusCode) : base(message: message, statusCode) { }
    }
}
