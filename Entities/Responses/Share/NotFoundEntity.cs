using System;

namespace Entities.Responses
{
    public class NotFoundEntity : ApiNotFoundResponse
    {
        public NotFoundEntity(string entity, Guid id)
        : base($"{entity} with id: {id} is not found in db.")
        {
        }
    }
}
