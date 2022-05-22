using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responses;

namespace Course.BLL.Services
{
    public interface IAccountService 
    {
        public Task<Response<UserResponse>> Register(RegisterRequest registerRequest);
        Task<Response<LoginResponse>> Login(LoginRequest loginRequest);
        Task<Response<LoginResponse>> AddRole(AddRoleRequest addRoleRequest);
    }
}
