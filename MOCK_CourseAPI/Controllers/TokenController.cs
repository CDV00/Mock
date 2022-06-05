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
    [Authorize]
    public class TokenController : ControllerBase
    {
        public readonly IAuthenticationService _authenticationService;
        public TokenController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<TokenDTO>>> Refresh([FromBody] TokenDTO tokenDto)
        {
            var tokenDtoToReturn = await
            _authenticationService.RefreshToken(tokenDto);
            return tokenDtoToReturn;
        }

    }
}
