namespace Course.BLL.Responsesnamespace
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public BaseResponse()
        {

        }
        public BaseResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public BaseResponse(bool isSuccess, string error, string message)
        {
            IsSuccess = isSuccess;
            Error = error;
            Message = message;
        }
    }
}
