using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        /// <summary>
        /// Get All Category and sub-category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Responses<CategoryResponse>>> GetAll()
        {
            var result = await _categoryService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Create new Category
        /// </summary>
        /// <param name="categoryRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response<CategoryResponse>>> Add(CategoryRequest categoryRequest)
        {
            var result = await _categoryService.Add(categoryRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Update an Category
        /// </summary>
        /// <param name="categoryUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Response<CategoryResponse>>> Update(CategoryUpdateRequest categoryUpdateRequest)
        {
            var result = await _categoryService.Update(categoryUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Delete an Category
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<BaseResponse>> Delete(Guid Id)
        {
            var result = await _categoryService.remove(Id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
