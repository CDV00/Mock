using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.Services.Abstraction;
using Course.BLL.Share.RequestFeatures;
using CourseAPI.Presentation.Controllers;
using Entities.Constants;
using Entities.Extension;
using Entities.ParameterRequest;
using Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ManagerController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public ManagerController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("get-all-user-by-role")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiOkResponses<UserDTO>>> GetAllUserByRole([FromQuery] UserParameter parameter)
        {
            var result = await _userService.GetAllUserByRole(parameter);
            if (!result.IsSuccess)
                return ProcessError(result);

            var coursePagedList = result.GetResult<PagedList<UserDTO>>();

            Response.Headers.Add(SystemConstant.PagedHeader,
                                 JsonSerializer.Serialize(coursePagedList.MetaData));

            return Ok(result);
        }



        [HttpPut("update-active")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiOkResponse<UserDTO>>> UpdateStatus(UpdateUserActiveRequest updateUserActiveRequest)
        {
            var result = await _userService.UpdateActive(updateUserActiveRequest);
            if (result.IsSuccess == false)
                return ProcessError(result);

            return Ok(result);
        }
    }
}
