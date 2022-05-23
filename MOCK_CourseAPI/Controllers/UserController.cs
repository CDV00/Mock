using Course.BLL.Requests;
using Course.BLL.Responses;
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

        [HttpGet("Get-Profile")]
        public Task<Response<UserProfileResponse>> GetProfile()
        {
            throw new NotImplementedException();
        }
        
        [HttpPut("Update-Profile")]
        public Task<BaseResponse> UpdateProfile(UpdateProfileRequest updateProfileRequest)
        {
            throw new NotImplementedException();
        }

        [HttpPut("Change-Password")]
        public Task<BaseResponse> ChagePassword(ChangePasswordRequest changePasswordRequest)
        {
            throw new NotImplementedException();
        }
    }
}
