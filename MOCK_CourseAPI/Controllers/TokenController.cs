using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Course.BLL.Services.Abstraction;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public readonly IAuthenticationService _authenticationService;
        public TokenController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="tokenDto"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<ActionResult<Response<TokenDTO>>> Refresh([FromBody] TokenDTO tokenDto)
        {
            var result = await
            _authenticationService.RefreshToken(tokenDto);

            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

    }
}
