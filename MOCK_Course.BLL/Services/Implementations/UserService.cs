using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICousesRepository _cousesRepository;
        private readonly ICourseReviewRepository _courseReviewRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        public UserService(UserManager<AppUser> userManager,
           IMapper mapper, IEnrollmentRepository enrollmentRepository, ICousesRepository cousesRepository, ICourseReviewRepository courseReviewRepository, ISubscriptionRepository subscriptionRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _cousesRepository = cousesRepository;
            _courseReviewRepository = courseReviewRepository;
            _subscriptionRepository = subscriptionRepository;
        }
        public async Task<Response<UserProfileDTO>> GetProfile(Guid id)
        {
            try
            {
                var userProfile = await _userManager.FindByIdAsync(id.ToString());
                var userProfileResponse = _mapper.Map<UserProfileDTO>(userProfile);

                userProfileResponse.TotalEnrollment = await _enrollmentRepository.BuildQuery()
                                                                                 .FilterByUserId(id)
                                                                                 .CountAsync();
                userProfileResponse.TotalCourse = await _cousesRepository.BuildQuery()
                                                                         .FilterByUserId(id)
                                                                         .CountAsync();
                userProfileResponse.TotalReviewCourse = await _courseReviewRepository.BuildQuery().FilterByUserId(id).CountAsync();

                userProfileResponse.TotalSubscription = await _subscriptionRepository.GetTotal(id);

                return new Response<UserProfileDTO>(
                    true,
                    userProfileResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<UserProfileDTO>(false, ex.Message, null);
            }
        }
        public async Task<BaseResponse> ChangePassword(Guid userId, ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

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
                return new Response<UserProfileDTO>(true, null, null);
            }
            catch (Exception ex)
            {
                return new Responses<UserProfileDTO>(false, ex.Message, null);
            }
        }


        public async Task<Response<UserProfileDTO>> UpdateProfile(Guid id, UpdateProfileRequest updateProfileRequest)
        {
            try
            {

                //var user = _mapper.Map<AppUser>(updateProfileRequest);

                var user = await _userManager.FindByIdAsync(id.ToString());
                _mapper.Map(updateProfileRequest, user);
                await _userManager.UpdateAsync(user);

                return new Response<UserProfileDTO>(
                    true,
                    _mapper.Map<UserProfileDTO>(user)
                );

            }
            catch (Exception ex)
            {
                return new Response<UserProfileDTO>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> CheckExistEmail(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                return new BaseResponse(false);
            }

            return new BaseResponse(true);
        }
    }
}