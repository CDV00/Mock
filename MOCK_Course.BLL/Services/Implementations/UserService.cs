using AutoMapper;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<Response<UserProfileResponse>> GetProfile(Guid id)
        {
            try
            {

                var userProfile = await _userManager.FindByIdAsync(id.ToString());

                return new Response<UserProfileResponse>(
                    true,
                    _mapper.Map<UserProfileResponse>(userProfile)
                );
            }
            catch (Exception ex)
            {
                return new Response<UserProfileResponse>(false, ex.Message, null);
            }
        }
        public async Task<BaseResponse> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(changePasswordRequest.Id.ToString());

                if (user == null)
                    return new BaseResponse(false, null, "can't find user");
                var checkPassword = _userManager.CheckPasswordAsync(user, changePasswordRequest.OldPassword);

                if (!checkPassword.Result)
                {
                    return new BaseResponse(false, null, "incorrect password!");
                }

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, changePasswordRequest.NewPassword);
                //update user password
                await _userManager.UpdateAsync(user);
                //return BadRequest("request is incorrect");
                return new Response<UserProfileResponse>(true, null, null);
            }
            catch (Exception ex)
            {
                return new Responses<UserProfileResponse>(false, ex.Message, null);
            }
        }


        public async Task<Response<UserProfileResponse>> UpdateProfile(UpdateProfileRequest updateProfileRequest)
        {
            try
            {

                //var user = _mapper.Map<AppUser>(updateProfileRequest);

                var user = await _userManager.FindByIdAsync(updateProfileRequest.Id.ToString());

                user.Fullname = updateProfileRequest.FirstName + updateProfileRequest.LastName;
                user.FirstName = updateProfileRequest.FirstName;
                user.LastName = updateProfileRequest.LastName;
                user.ProfileLink = updateProfileRequest.ProfileLink;
                user.FacebookLink = updateProfileRequest.FacebookLink;
                user.LinkedlnLink = updateProfileRequest.LinkedlnLink;
                user.YoutubeLink = updateProfileRequest.YoutubeLink;
                user.HeadLine = updateProfileRequest.HeadLine;
                user.Description = updateProfileRequest.Description;
                await _userManager.UpdateAsync(user);

                return new Response<UserProfileResponse>(
                    true,
                    _mapper.Map<UserProfileResponse>(user)
                );

            }
            catch (Exception ex)
            {
                return new Response<UserProfileResponse>(false, ex.Message, null);
            }
        }

    }
}