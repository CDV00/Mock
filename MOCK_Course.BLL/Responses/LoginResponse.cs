using Course.BLL.DTO;

namespace Course.BLL.DTO
{
    public class LoginDTO
    {
        public TokenDto Token { get; set; }
        public UserResponse UserResponse { get; set; }

        public LoginDTO(TokenDto token, UserResponse userResponse)
        {
            Token = token;
            UserResponse = userResponse;
        }
    }

    public class UserResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
    public class RegisterResponse
    {
        //public RegisterResponse(string token, UserResponse userResponse) : base(token, userResponse)
        //{
        //}

    }
}
