using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;

namespace Course.BLL.Services
{
    public interface IAccountService 
    {
        public Task<Response<RegisterResponse>> Register(RegisterRequest registerRequest);
        Task<Response<LoginResponse>> Login(LoginRequest loginRequest);
    }
}
