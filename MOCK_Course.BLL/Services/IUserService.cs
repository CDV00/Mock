using Course.BLL.Requests;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IUserService
    {
        Task<Response<UserProfileResponse>> GetProfile(Guid id);
        Task<Response<UserProfileResponse>> UpdateProfile(Guid userId, UpdateProfileRequest updateProfileRequest);
        Task<BaseResponse> ChangePassword(Guid userId, ChangePasswordRequest changePasswordRequest);
    }
}
