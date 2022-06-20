using System;

namespace Entities.Responses
{
    public class DuplicateCartResponse : ApiUnprocessableResponse
    {
        public DuplicateCartResponse(Guid id) : base(message: $"Already have course with id:{id}", null) { }
    }
}
