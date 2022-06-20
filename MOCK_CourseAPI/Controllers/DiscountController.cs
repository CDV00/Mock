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
using CourseAPI.Presentation.Controllers;
using Entities.Responses;
using Entities.Constants;
using Course.BLL.Share.RequestFeatures;
using Entities.ParameterRequest;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Instructor")]
    public class DiscountController : ApiControllerBase
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
        public async Task<ActionResult<ApiOkResponses<PagedList<DiscountDTO_>>>> GetAllDiscount([FromQuery] DiscountParameters parameter)
        {
            var userId = User.GetUserId();
            var result = await _discountService.GetAllDiscount(userId, parameter);
            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Add new Discount
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        /// <response code="1013">If Already have discount in the period of time</response>
        [HttpPost]
        [ProducesResponseType(1013)]
        //[ServiceFilter(typeof(ValidationCourseForDiscountExistAttribute))]
        //[ServiceFilter(typeof(ValidationDateTimeForDiscountAttribute))]
        public async Task<ActionResult<Response<DiscountDTO_>>> Add(DiscountForCreateRequest discount)
        {
            var result = await _discountService.Add(discount);
            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Update Discount
        /// </summary>
        /// <returns></returns>
        /// <response code="1013">If Already have discount in the period of time</response>
        [HttpPut()]
        [ProducesResponseType(1013)]
        //[ServiceFilter(typeof(ValidationCourseForDiscountExistAttribute))]
        //[ServiceFilter(typeof(ValidationDiscountExistAttribute))]
        public async Task<ActionResult<Response<DiscountDTO_>>> Update(Guid id, DiscountForUpdateRequest discountForUpdate)
        {
            //var discount = HttpContext.Items[DiscountConstant.Name] as Discount;
            var result = await _discountService.Update(id, discountForUpdate);
            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Delete Discount
        /// https://gambolthemes.net/html-items/cursus_main_demo/instructor_courses.html#
        /// </summary>
        /// <param name="id">Id Discount</param>
        /// <returns></returns>
        [HttpDelete()]
        //[ServiceFilter(typeof(ValidationCourseForDiscountExistAttribute))]
        //[ServiceFilter(typeof(ValidationDiscountExistAttribute))]
        public async Task<ActionResult<BaseResponse>> Delete(Guid id)
        {
            //var discount = HttpContext.Items[DiscountConstant.Name] as Discount;
            var result = await _discountService.Remove(id);
            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }
    }
}
