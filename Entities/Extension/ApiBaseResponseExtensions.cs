using Entities.Responses;
using System.Collections.Generic;

namespace Entities.Extension
{
    public static class ApiBaseResponseExtensions
    {
        public static TResultType GetResult<TResultType>(this ApiBaseResponse
        apiBaseResponse) =>
        ((ApiOkResponse<TResultType>)apiBaseResponse).data;

        public static List<TResultType> GetResults<TResultType>(this ApiBaseResponse
      apiBaseResponse) =>
      ((ApiOkResponses<TResultType>)apiBaseResponse).Data;
    }
}
