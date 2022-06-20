namespace Course.BLL.DTO
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public BaseResponse()
        {

        }
        public BaseResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public BaseResponse(bool isSuccess, string message, string statusCode)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
