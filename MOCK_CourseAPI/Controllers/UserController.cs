using Course.BLL.Requests;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using System.Text.Json;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Get profile of user. If userId == null, it will get userId of user is login in ( if user not authentication and not pass userId will return error)
        /// https://gambolthemes.net/html-items/cursus_main_demo/my_instructor_profile_view.html
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-User")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> GetUser(Guid? userId)
        {
            if (userId == null)
                userId = User.GetUserId();
            if (userId == null)
                return BadRequest(new Response<UserDTO>(false, "User Id is null or not authentication", null));

            var result = await _userService.GetUserProfile(userId.GetValueOrDefault());
            if (result.IsSuccess == false)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Update profile of user
        /// https://gambolthemes.net/html-items/cursus_main_demo/setting.html
        /// </summary>
        /// <param name="updateProfileRequest"></param>
        /// <returns></returns>
        [HttpPut("Update-User")]
        public async Task<ActionResult<Response<UserDTO>>> UpdateProfile([FromBody] UpdateProfileRequest updateProfileRequest)
        {
            var userId = User.GetUserId();
            var result = await _userService.UpdateProfile(userId, updateProfileRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Change password
        /// <param name="changePasswordRequest"></param>
        /// <returns></returns>
        [HttpPut("Change-Password")]
        public async Task<ActionResult<BaseResponse>> ChagePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var userId = User.GetUserId();

            var result = await _userService.ChangePassword(userId, changePasswordRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        ///  Check exist email
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpGet("Check-Exist-Email")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> CheckExistEmail(string Email)
        {
            var result = await _userService.CheckExistEmail(Email);
            return Ok(result);
        }

        /// <summary>
        /// Get Popular Instructor
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-Popular-Instructor")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<UserDTO>>> GetPopularInstructor([FromQuery] UserParameter userParameter)
        {
            var result = await _userService.GetPopularInstructor(userParameter);

            Response.Headers.Add("X-Pagination",
                               JsonSerializer.Serialize(result.MetaData));

            return Ok(result);
        }
    }
}
