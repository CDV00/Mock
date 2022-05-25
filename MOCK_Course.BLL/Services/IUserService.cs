using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IUserService
    {
        Task<Response<UserProfileResponse>> GetProfile(Guid id);
        Task<Response<UserProfileResponse>> UpdateProfile(Guid Id, UpdateProfileRequest updateProfileRequest);
        Task<BaseResponse> ChangePassword(Guid Id, ChangePasswordRequest changePasswordRequest);
    }
}
