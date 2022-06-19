using System.Collections.Generic;

namespace Entities.Responses
{
    public sealed class ApiOkResponses<TResult> : ApiBaseResponse
    {
        public List<TResult> Data { get; set; }
        public ApiOkResponses(List<TResult> result)
        : base(true)
        {
            Data = result;
        }
    }
}
