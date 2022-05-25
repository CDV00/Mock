using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Get-Profile/{id}")]
        public async Task<ActionResult<BaseResponse>> GetProfile(Guid id)
        {
            var result = await _userService.GetProfile(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("Update-Profile")]
        public async Task<ActionResult<BaseResponse>> UpdateProfile(UpdateProfileRequest updateProfileRequest)
        {
            var result = await _userService.UpdateProfile(updateProfileRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("Change-Password")]
        public async Task<ActionResult<BaseResponse>> ChagePassword(ChangePasswordRequest changePasswordRequest)
        {
            var result = await _userService.ChangePassword(changePasswordRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
