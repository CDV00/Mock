using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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
        [AllowAnonymous]
        public async Task<ActionResult<Responses<CategoryResponse>>> GetAll()
        {
            var result = await _categoryService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        ///// <summary>
        ///// Create new Category
        ///// don't have Page yet
        ///// </summary>
        ///// <param name="categoryRequest">CategoryResponse</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult<Response<CategoryResponse>>> Add(CategoryRequest categoryRequest)
        //{
        //    var result = await _categoryService.Add(categoryRequest);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}

        ///// <summary>
        ///// Update an Category
        ///// don't have Page yet
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="categoryUpdateRequest">CategoryUpdate</param>
        ///// <returns></returns>
        //[HttpPut("{id:guid}")]
        //public async Task<ActionResult<Response<CategoryResponse>>> Update(Guid id, CategoryUpdateRequest categoryUpdateRequest)
        //{
        //    var result = await _categoryService.Update(id, categoryUpdateRequest);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}

        ///// <summary>
        ///// Delete an Category
        ///// don't have Page yet
        ///// </summary>
        ///// <param name="Id">Id Category</param>
        ///// <returns></returns>
        //[HttpDelete("{id:guid}")]
        //public async Task<ActionResult<BaseResponse>> Delete(Guid Id)
        //{
        //    var result = await _categoryService.remove(Id);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
    }
}
