using Course.BLL.Requests;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;
using Course.BLL.Share.RequestFeatures;
using Entities.ParameterRequest;
using Entities.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface IUserService
    {
        Task<Response<UserDTO>> GetUserProfile(Guid id);
        Task<Response<UserDTO>> UpdateProfile(Guid userId, UpdateProfileRequest updateProfileRequest);
        Task<BaseResponse> ChangePassword(Guid userId, ChangePasswordRequest changePasswordRequest);
        Task<BaseResponse> CheckExistEmail(string Email);
        Task<PagedList<UserDTO>> GetPopularInstructor(UserParameter userParameter, Guid userid);
        Task<Response<BaseResponse>> UpdateActive(UpdateUserActiveRequest updateUserActiveRequest);
        //Task<Responses<UserDTO>> GetAllUserByRole(string role);
        Task<ApiBaseResponse> GetAllUserByRole(UserParameter parameter);
    }
}
