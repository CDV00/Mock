using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ManagerController : ControllerBase
    {
        private readonly IUserService _userService;
        public ManagerController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("get-all-user-by-role")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse>> GetAllUserByRole(string role)
        {
            var result = await _userService.GetAllUserByRole(role);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("update-active")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse>> UpdateStatus(UpdateUserActiveRequest updateUserActiveRequest)
        {
            var result = await _userService.UpdateActive(updateUserActiveRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
