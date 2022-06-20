using System;

namespace Entities.Responses
{
    public class ExistDiscountTimeResponse : ApiUnprocessableResponse
    {
        public ExistDiscountTimeResponse(DateTime startDate, DateTime endDate, Guid courseId) : base(message: $"Already have discount in the period of time with start date: {startDate}, end date: {endDate} for course id: {courseId}", statusCode: 1013) { }
    }
}
