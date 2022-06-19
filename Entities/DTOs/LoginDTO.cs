using Course.BLL.DTO;

namespace Course.BLL.DTO
{
    public class LoginDTO
    {
        public TokenDTO Token { get; set; }
        public UserDTO UserResponse { get; set; }

        public LoginDTO(TokenDTO token, UserDTO userResponse)
        {
            Token = token;
            UserResponse = userResponse;
        }
    }
}
