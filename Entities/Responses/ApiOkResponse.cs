namespace Entities.Responses
{
    public class ApiOkResponse<TResult> : ApiBaseResponse
    {
        public TResult data { get; set; }
        public ApiOkResponse(TResult result)
        : base(true)
        {
            data = result;
        }
    }
}
