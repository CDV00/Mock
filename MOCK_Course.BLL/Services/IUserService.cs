using Course.BLL.Requests;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IUserService
    {
        Task<Response<UserProfileResponse>> GetProfile(Guid id);
        Task<Response<UserProfileResponse>> UpdateProfile(UpdateProfileRequest updateProfileRequest);
        Task<BaseResponse> ChangePassword(ChangePasswordRequest changePasswordRequest);
    }
}
