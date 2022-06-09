using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.Services.Abstraction;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICousesRepository _cousesRepository;
        private readonly ICourseReviewRepository _courseReviewRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserRepository _userRepository;
        public UserService(UserManager<AppUser> userManager,
           IMapper mapper, IEnrollmentRepository enrollmentRepository, ICousesRepository cousesRepository, ICourseReviewRepository courseReviewRepository, ISubscriptionRepository subscriptionRepository, IUserRepository userRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _cousesRepository = cousesRepository;
            _courseReviewRepository = courseReviewRepository;
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
        }
        public async Task<Response<UserDTO>> GetUserProfile(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());

                var userResponse = _mapper.Map<UserDTO>(user);


                //userProfileResponse.TotalEnrollment = await _enrollmentRepository.BuildQuery()
                //                                                                 .FilterByUserId(id)
                //                                                                 .CountAsync();
                //userProfileResponse.TotalCourse = await _cousesRepository.BuildQuery()
                //                                                         .FilterByUserId(id)
                //                                                         .CountAsync();
                //userProfileResponse.TotalReviewCourse = await _courseReviewRepository.BuildQuery().FilterByUserId(id).CountAsync();

                //userProfileResponse.TotalSubscription = await _subscriptionRepository.GetTotal(id);

                return new Response<UserDTO>(
                    true,
                    userResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<UserDTO>(false, ex.Message, null);
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
                return new Response<UserDTO>(true, null, null);
            }
            catch (Exception ex)
            {
                return new Responses<UserDTO>(false, ex.Message, null);
            }
        }


        public async Task<Response<UserDTO>> UpdateProfile(Guid id, UpdateProfileRequest updateProfileRequest)
        {
            try
            {

                //var user = _mapper.Map<AppUser>(updateProfileRequest);

                var user = await _userManager.FindByIdAsync(id.ToString());
                _mapper.Map(updateProfileRequest, user);
                user.UpdatedAt = DateTime.Now;
                await _userManager.UpdateAsync(user);

                return new Response<UserDTO>(
                    true,
                    _mapper.Map<UserDTO>(user)
                );

            }
            catch (Exception ex)
            {
                return new Response<UserDTO>(false, ex.Message, null);
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
        //
        public async Task<Responses<UserDTO>> GetPopularInstructor()
        {
            try
            {
                string RoleName = UserRoles.Instructor;
                var user = await _userRepository.BuildQuery()
                                                        .FilterByRole(RoleName)
                                                        .SortBySubscription()
                                                        .ToListAsync(u => _mapper.Map<UserDTO>(u));

                return new Responses<UserDTO>(true, user);
            }
            catch (Exception ex)
            {
                return new Responses<UserDTO>(false, ex.Message, null);
            }
        }
    }
}