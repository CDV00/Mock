using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        /// Register new account
        /// </summary>
        /// <remarks>
        ///if categoryId(category of course) == null then user role is student, 
        /// otherwise user role is Instructor
        /// </remarks>
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
        /// Login 
        /// </summary>
        /// <remarks>
        /// ## Account Admin:
        /// email: admin123@gmail.com         
        ///  password: 1aA*1aA*
        /// </remarks>
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
            /*var originValue = Request.Headers["Origin"].FirstOrDefault();

            // or

            StringValues originValues;
            Request.Headers.TryGetValue("Origin", out originValues);
            Console.WriteLine(originValue);*/

            var result = await _userService.ForgetPassWord(email);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="working"> Register, forget password</param>
        /// <returns></returns>
        [HttpPost("Reset-Code-Number")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetCodeNumber(string email, string working)
        {
            var result = await _userService.ResetCodeNumber(email, working);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPost("Confirm")]
        [AllowAnonymous]
        public async Task<ActionResult> Confirm(string email, string codeNumber)
        {
            var result = await _userService.Confirm(email, codeNumber);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
