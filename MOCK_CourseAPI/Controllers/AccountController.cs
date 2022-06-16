using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Course.BLL.DTO;
using Course.BLL.Services.Abstraction;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;
using System;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _userService;
        public AuthenticationController(IAuthenticationService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register a account, if categoryId (category of course) == null then user role is student, 
        /// otherwise user role is Instructor
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns>token and User Information</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> Register([FromBody] RegisterRequest registerRequest)
        {
            if (ModelState.IsValid)
            {

            }
            var result = await _userService.Register(registerRequest);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Login with email and password
        /// https://gambolthemes.net/html-items/cursus_main_demo/sign_in.html
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>Token and user Information</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginDTO>> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _userService.Login(loginRequest);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// forget Password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("ForgetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> ForgetPassword(string email)
        {
            var originValue = Request.Headers["Origin"].FirstOrDefault();

            // or

            StringValues originValues;
            Request.Headers.TryGetValue("Origin", out originValues);
            Console.WriteLine(originValue);

            var result = await _userService.ForgetPassWord(email, originValue);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var result = await _userService.ResetPassWord(resetPasswordRequest);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// login with google or facebook
        /// </summary>
        /// <param name="externalLoginResquest">Provider(google or facebook) and token</param>
        /// <returns></returns>
        [HttpPost("ExternalLogin")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginDTO>> ExternalLogin(ExternalLoginResquest externalLoginResquest)
        {
            var result = await _userService.ExternalLogin(externalLoginResquest);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
