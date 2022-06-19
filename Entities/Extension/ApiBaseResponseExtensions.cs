using Entities.Responses;

namespace Entities.Extension
{
    public static class ApiBaseResponseExtensions
    {
        public static TResultType GetResult<TResultType>(this ApiBaseResponse
        apiBaseResponse) =>
        ((ApiOkResponse<TResultType>)apiBaseResponse).data;
    }
}
