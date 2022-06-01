using Course.BLL.Requests;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IUserService
    {
        Task<Response<UserProfileDTO>> GetProfile(Guid id);
        Task<Response<UserProfileDTO>> UpdateProfile(Guid userId, UpdateProfileRequest updateProfileRequest);
        Task<BaseResponse> ChangePassword(Guid userId, ChangePasswordRequest changePasswordRequest);
        Task<BaseResponse> CheckExistEmail(string Email);
    }
}
