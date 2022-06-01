using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.DTO;
using Course.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Response<TokenDto>>> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await
            _authenticationService.RefreshToken(tokenDto);
            return tokenDtoToReturn;
        }

    }
}
