using System;

namespace Entities.Responses
{
    public class CategoryNotFoundResponse : ApiNotFoundResponse
    {
        public CategoryNotFoundResponse(Guid id)
        : base($"category with id: {id} is not found in db.")
        {
        }
    }
}
