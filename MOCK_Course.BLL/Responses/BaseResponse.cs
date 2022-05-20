namespace Course.BLL.Responses
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public int? Error { get; set; }
        public string Message { get; set; }
        public BaseResponse()
        {

        }
        public BaseResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public BaseResponse(bool isSuccess, int? error, string message)
        {
            IsSuccess = isSuccess;
            Error = error;
            Message = message;
        }
    }
}
