using Course.BLL.Requests;
using Course.BLL.DTO;
using Course.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        /// Get profile of user
        /// https://gambolthemes.net/html-items/cursus_main_demo/my_instructor_profile_view.html
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get-Profile/{id:guid}")]
        public async Task<ActionResult<UserProfileResponse>> GetProfile(Guid id)
        {
            var result = await _userService.GetProfile(id);
            if (result.IsSuccess == false)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Update profile of user
        /// https://gambolthemes.net/html-items/cursus_main_demo/setting.html
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateProfileRequest"></param>
        /// <returns></returns>
        [HttpPut("Update-Profile")]
        public async Task<ActionResult<UserProfileResponse>> UpdateProfile([FromBody] UpdateProfileRequest updateProfileRequest)
        {
            var result = await _userService.UpdateProfile(updateProfileRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Change passowrd by User Id
        /// not Page UI yet!
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <returns></returns>
        [HttpPut("Change-Password")]
        public async Task<ActionResult<BaseResponse>> ChagePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var result = await _userService.ChangePassword(changePasswordRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
