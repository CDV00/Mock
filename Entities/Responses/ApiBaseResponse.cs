namespace Entities.Responses
{
    public class ApiBaseResponse
    {
        public bool IsSuccess { get; set; }
        public ApiBaseResponse(bool success) => IsSuccess = success;
    }
}
