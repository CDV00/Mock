using System.Threading.Tasks;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Course.BLL.Responses;
using Course.BLL.Requests;
using System;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Instructor")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        /// <summary>
        /// Get All discount
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
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
        /// <param name="discountCreateRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response<DiscountDTO_>>> Add(DiscountForCreateRequest discountCreateRequest)
        {
            var result = await _discountService.Add(discountCreateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Update Discount
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        public async Task<ActionResult<Response<DiscountDTO_>>> Update(Guid id, DiscountForUpdateRequest discountUpdateRequest)
        {
            var result = await _discountService.Update(id, discountUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Delete Discount
        /// https://gambolthemes.net/html-items/cursus_main_demo/instructor_courses.html#
        /// </summary>
        /// <param name="id">Id Discount</param>
        /// <returns></returns>
        [HttpDelete()]
        public async Task<ActionResult<BaseResponse>> Delete(Guid id)
        {
            var result = await _discountService.Remove(id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
