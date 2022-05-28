using Course.BLL.DataTransferObjects;

namespace Course.BLL.Responsesnamespace
{
    public class LoginResponse
    {
        public TokenDto Token { get; set; }
        public UserResponse UserResponse { get; set; }

        public LoginResponse(TokenDto token, UserResponse userResponse)
        {
            Token = token;
            UserResponse = userResponse;
        }
    }

    public class UserResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
    }
    public class RegisterResponse
    {
        //public RegisterResponse(string token, UserResponse userResponse) : base(token, userResponse)
        //{
        //}
    }
}
