using Course.BLL.Requests;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services.Abstraction
{
    public interface IUserService
    {
        Task<Response<UserDTO>> GetUserProfile(Guid id);
        Task<Response<UserDTO>> UpdateProfile(Guid userId, UpdateProfileRequest updateProfileRequest);
        Task<BaseResponse> ChangePassword(Guid userId, ChangePasswordRequest changePasswordRequest);
        Task<BaseResponse> CheckExistEmail(string Email);
    }
}
