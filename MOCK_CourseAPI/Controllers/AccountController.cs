using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _userService;
        public AccountController(IAccountService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register a account
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<Response<UserResponse>>> Register(RegisterRequest registerRequest)
        {
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
        /// <param name="loginRequest"></param>
        /// <returns>Token and user</returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var result = await _userService.Login(loginRequest);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddRole")]
        public async Task<ActionResult<LoginResponse>> AddRole(AddRoleRequest addRoleRequest)
        {
            var result = await _userService.AddRole(addRoleRequest);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
