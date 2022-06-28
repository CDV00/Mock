using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.Services.Abstraction;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Entities.Constants;
using Entities.DTOs;
using Entities.ParameterRequest;
using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _cousesRepository;
        private readonly ICourseReviewRepository _courseReviewRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ICourseService _courseService;
        private readonly IUserRepository _userRepository;
        private readonly IDipositRepository _dipositRepository;
        public UserService(UserManager<AppUser> userManager,
           IMapper mapper, IEnrollmentRepository enrollmentRepository, ICourseRepository cousesRepository, ICourseReviewRepository courseReviewRepository, ISubscriptionRepository subscriptionRepository, IUserRepository userRepository, ISubscriptionService subscriptionService, ICourseService courseService, IDipositRepository dipositRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _cousesRepository = cousesRepository;
            _courseReviewRepository = courseReviewRepository;
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
            _subscriptionService = subscriptionService;
            _courseService = courseService;
            _dipositRepository = dipositRepository;
        }

        // Get All User(role):Full Name, Birthday,... IsActive
        /*public async Task<Responses<UserDTO>> GetAllUserByRole(string role)
        {
            try
            {
                var users = await _userRepository.BuildQuery()
                                                 .FilterByRole(role)
                                                 .ToListAsync(u => _mapper.Map<UserDTO>(u));
                foreach (var user in users)
                {
                    user.Role = role;
                }    
                return new Responses<UserDTO>(true, users);
            }
            catch (Exception ex)
            {
                return new Responses<UserDTO>(false, ex.Message, null);
            }
        }*/
        public async Task<ApiBaseResponse> GetAllUserByRole(UserParameter parameter)
        {
            
            var users = await _userRepository.GetAllUserByRole(parameter);

            return new ApiOkResponse<PagedList<UserDTO>>(users);
        }
        // Update User: Id, IsActive
        public async Task<Response<BaseResponse>> UpdateActive(UpdateUserActiveRequest updateUserActiveRequest)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(updateUserActiveRequest.Id.ToString());
                if (user == null)
                {
                    return new Response<BaseResponse>(false, "can't find user", null);
                }
                user.IsActive = updateUserActiveRequest.IsActive;
                await _userManager.UpdateAsync(user);
                return new Response<BaseResponse>(
                    true
                );

            }
            catch (Exception ex)
            {
                return new Response<BaseResponse>(false, ex.Message, null);
            }
        }
        public async Task<Response<UserDTO>> GetUserProfile(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());

                var userResponse = _mapper.Map<UserDTO>(user);
                userResponse.isSubscribed = await _subscriptionService.IsSubscribed(userResponse.Id, userResponse.Id) == null ? false : true;
                userResponse.TotalSubcripbers = (await _subscriptionService.GetTotalSubscriber(userResponse.Id)).data;
                userResponse.TotalCourses = (await _courseService.GetTotalCourseOfUser(userResponse.Id)).data;

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

            if (user == null || !user.EmailConfirmed || user.Fullname == null)
            {
                return new BaseResponse(false);
            }

            return new BaseResponse(true);
        }

        public async Task<PagedList<UserDTO>> GetPopularInstructor(UserParameter userParameter, Guid userid)
        {
            string RoleName = UserRolesConstant.Instructor;
            var users = await _userRepository.BuildQuery()
                                             .FilterByRole(RoleName)
                                             .FilterByName(userParameter.Keyword)
                                             .SortBySubscription(userParameter.IsPopular)
                                             .Skip((userParameter.PageNumber - 1) * userParameter.PageSize)
                                             .Take(userParameter.PageSize)
                                             .ToListAsync(u => _mapper.Map<UserDTO>(u));

            var count = await _userRepository.BuildQuery()
                                             .FilterByRole(RoleName)
                                             .FilterByName(userParameter.Keyword)
                                             .CountAsync();

            //Get Total Subscribers & Total Courses
            for (var i = 0; i < users.Count; i++)
            {
                users[i].TotalSubcripbers = (await _subscriptionService.GetTotalSubscriber(users[i].Id)).data;
                users[i].TotalCourses = (await _courseService.GetTotalCourseOfUser(users[i].Id)).data;

                users[i].isSubscribed = (await _subscriptionService.IsSubscribed(userid, users[i].Id)).data == null ? false : true;
            }

            var pageList = new PagedList<UserDTO>(users, count, userParameter.PageNumber, userParameter.PageSize);
            return pageList;
        }
        public async Task<PagedList<DepositDTO>> GetDeposit(DepositParameters depositParameters, Guid userid)
        {
            var users = await _dipositRepository.BuildQuery()
                                             .FilterByUser(userid)
                                             .FilterDateStart(depositParameters.startDate)
                                             .FilterDateEnd(depositParameters.endDate)
                                             .ApplySort(depositParameters.Orderby)
                                             .Skip((depositParameters.PageNumber - 1) * depositParameters.PageSize)
                                             .Take(depositParameters.PageSize)
                                             .ToListAsync(d => _mapper.Map<DepositDTO>(d));

            var count = await _dipositRepository.BuildQuery()
                                             .FilterByUser(userid)
                                             .CountAsync();
            var pageList = new PagedList<DepositDTO>(users, count, depositParameters.PageNumber, depositParameters.PageSize);
            return pageList;
        }
    }
}