namespace Course.BLL.Responsesnamespace
{
    public class LoginResponse
    {
        public string Token  { get; set; }
        public UserResponse UserResponse { get; set; }

        public LoginResponse(string token, UserResponse userResponse)
        {
            Token = token;
            UserResponse = userResponse;
        }
    }

    public class UserResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }   
        public string Id { get;set; }
        public string Role { get; set; } 
    }
    public class RegisterResponse : LoginResponse
    {
        public RegisterResponse(string token, UserResponse userResponse) : base(token, userResponse)
        {
        }
    }
}
