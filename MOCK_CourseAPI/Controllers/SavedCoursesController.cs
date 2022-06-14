using Course.BLL.Requests;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using System.Text.Json;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SavedCoursesController : ControllerBase
    {
        private readonly ISavedCoursesService _savedCoursesService;
        public SavedCoursesController(ISavedCoursesService savedCoursesService)
        {
            _savedCoursesService = savedCoursesService;
        }


        /// <summary>
        /// Get all saved courses with paging and filter
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="savedCoursesParameters"></param>
        /// <returns></returns>
        [HttpGet("Get-all-saved-courses")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<SavedCoursesDTO>>> GetAllSavedCourses([FromQuery] SavedCoursesParameters savedCoursesParameters)
        {
            var userId = User.GetUserId();
            var result = await _savedCoursesService.GetAll(userId,savedCoursesParameters);

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(result.MetaData));

            return Ok(new Responses<SavedCoursesDTO>(true, result));
        }

        
        /// <summary>
        /// Add Saved Courses
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Responses<SavedCoursesDTO>>> Create(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _savedCoursesService.Add(userId, courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("is-saved-courses")]
        public async Task<ActionResult<BaseResponse>> IsSavedCourses(Guid courseId)
        {
            var userId = User.GetUserId();
            var result = await _savedCoursesService.IsSaveCourses(userId, courseId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Remove Saved Course by Id
        /// https://gambolthemes.net/html-items/cursus_main_demo/shopping_cart.html
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete()]
        public async Task<ActionResult<BaseResponse>> Remove(Guid Id)
        {
            var result = await _savedCoursesService.Remove(Id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
        /// <summary>
        /// Remove All Save Course By User
        /// </summary>
        /// <returns></returns>
        [HttpDelete("remove-all")]
        public async Task<ActionResult<BaseResponse>> RemoveAll()
        {
            var userId = User.GetUserId();
            var result = await _savedCoursesService.RemoveAll(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
