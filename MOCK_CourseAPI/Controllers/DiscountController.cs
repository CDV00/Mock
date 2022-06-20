using System.Threading.Tasks;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Course.BLL.Responses;
using Course.BLL.Requests;
using System;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using Contracts;
using CourseAPI.ActionFilters;
using Course.DAL.Models;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Instructor")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly ILoggerManager _logger;
        public DiscountController(IDiscountService discountService, ILoggerManager logger)
        {
            _discountService = discountService;
            _logger = logger;
        }

        /// <summary>
        /// Get All discount
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Responses<DiscountDTO_>>> GetAllDiscount()
        {
            var userId = User.GetUserId();
            var result = await _discountService.GetAllDiscount(userId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Add Discount
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationCourseForDiscountExistAttribute))]
        public async Task<ActionResult<Response<DiscountDTO_>>> Add(Guid courseId, DiscountForCreateRequest discount)
        {
            var course = HttpContext.Items["course"] as Courses;
            var result = await _discountService.Add(discount, course);

            return Ok(result);
        }

        /// <summary>
        /// Update Discount
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationCourseForDiscountExistAttribute))]
        [ServiceFilter(typeof(ValidationDiscountExistAttribute))]
        public async Task<ActionResult<Response<DiscountDTO_>>> Update(Guid id, Guid courseId, DiscountForUpdateRequest discountForUpdate)
        {
            var discount = HttpContext.Items["Discount"] as Discount;
            var result = await _discountService.Update(discount, courseId, discountForUpdate);

            return Ok(result);
        }

        /// <summary>
        /// Delete Discount
        /// https://gambolthemes.net/html-items/cursus_main_demo/instructor_courses.html#
        /// </summary>
        /// <param name="id">Id Discount</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationCourseForDiscountExistAttribute))]
        [ServiceFilter(typeof(ValidationDiscountExistAttribute))]
        public async Task<ActionResult<BaseResponse>> Delete(Guid id, Guid courseId)
        {
            var discount = HttpContext.Items["Discount"] as Discount;

            var result = await _discountService.Remove(discount);
            return Ok(result);
        }
    }
}
