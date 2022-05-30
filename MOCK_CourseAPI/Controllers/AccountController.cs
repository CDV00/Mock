using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.DTO;
using Course.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Course.BLL.DataTransferObjects;

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
        /// https://gambolthemes.net/html-items/cursus_main_demo/sign_up.html
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns>token and User Information</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse>> Register([FromBody] RegisterRequest registerRequest)
        {
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
    }
}
