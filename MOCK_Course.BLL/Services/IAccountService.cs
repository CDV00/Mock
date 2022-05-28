using System.Threading.Tasks;
using Course.BLL.DataTransferObjects;
using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;

namespace Course.BLL.Services
{
    public interface IAuthenticationService
    {
        Task<Response<UserResponse>> Register(RegisterRequest registerRequest);
        Task<Response<LoginResponse>> Login(LoginRequest loginRequest);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<bool> ValidateUser(LoginRequest userForAuth);
        Task<string> CreateToken();
        Task<Response<TokenDto>> RefreshToken(TokenDto tokenDto);
    }
}
