﻿using AutoMapper;
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
        public UserService(UserManager<AppUser> userManager,
            IMapper mapper, IEnrollmentRepository enrollmentRepository, ICousesRepository cousesRepository, ICourseReviewRepository courseReviewRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _cousesRepository = cousesRepository;
            _courseReviewRepository = courseReviewRepository;

        }
        public async Task<Response<UserProfileResponse>> GetProfile(Guid id)
        {
            try
            {
                var userProfile = await _userManager.FindByIdAsync(id.ToString());
                var userProfileResponse = _mapper.Map<UserProfileResponse>(userProfile);

                userProfileResponse.TotalEnrollment = await _enrollmentRepository.GetTotal(id);
                userProfileResponse.TotalCourse = await _cousesRepository.GetTotal(id);
                userProfileResponse.TotalReviewCourse = await _courseReviewRepository.GetTotal(id);

                return new Response<UserProfileResponse>(
                    true,
                    userProfileResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<UserProfileResponse>(false, ex.Message, null);
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
                return new Response<UserProfileResponse>(true, null, null);
            }
            catch (Exception ex)
            {
                return new Responses<UserProfileResponse>(false, ex.Message, null);
            }
        }


        public async Task<Response<UserProfileResponse>> UpdateProfile(Guid id, UpdateProfileRequest updateProfileRequest)
        {
            try
            {

                //var user = _mapper.Map<AppUser>(updateProfileRequest);

                var user = await _userManager.FindByIdAsync(id.ToString());
                _mapper.Map(updateProfileRequest, user);
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