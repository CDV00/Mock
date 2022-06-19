namespace Entities.Responses
{
    public abstract class ApiUnprocessableResponse : ApiBaseResponse
    {
        public string Message { get; set; }
        public int? StatusCode { get; set; }
        public ApiUnprocessableResponse(string message, int? statusCode)
        : base(false)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
