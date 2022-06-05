using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;

namespace Course.BLL.Services.Abstraction
{
    public interface IAuthenticationService
    {
        Task<BaseResponse> Register(RegisterRequest registerRequest);
        Task<Response<LoginDTO>> Login(LoginRequest loginRequest);
        Task<TokenDTO> CreateToken(bool populateExp);
        Task<bool> ValidateUser(LoginRequest userForAuth);
        Task<string> CreateToken();
        Task<Response<TokenDTO>> RefreshToken(TokenDTO tokenDto);
        Task<Response<BaseResponse>> ForgetPassWord(string email);
        Task<Response<BaseResponse>> ResetPassWord(ResetPasswordRequest resetPasswordRequest);
        Task<Response<LoginDTO>> ExternalLogin(ExternalLoginResquest externalLoginResquest);
    }
}
