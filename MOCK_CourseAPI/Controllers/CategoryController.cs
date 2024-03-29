﻿using System.Threading.Tasks;
using System.Threading.Tasks;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Course.BLL.Services.Abstraction;
using System;
using Course.BLL.Requests;
using Entities.ParameterRequest;
using Entities.Constants;
using System.Text.Json;

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
        public async Task<ActionResult<Responses<CategoryDTO_>>> GetAll()
        {
            var result = await _categoryService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Get top sub category with the most course
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-sub-category")]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<CategoryDTO_>>> GetAllSubcategory([FromQuery] CategoryParameters parameters)
        {
            var result = await _categoryService.GetSubCategory(parameters);
            if (result.IsSuccess == false)
                return BadRequest(result);

            Response.Headers.Add(SystemConstant.PagedHeader,
                             JsonSerializer.Serialize(result.data.MetaData));

            return Ok(result);
        }

        /// <summary>
        /// Create new Category
        /// </summary>
        /// <param name="categoryRequest">CategoryResponse</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response<CategoryDTO_>>> Add(CategoryRequest categoryRequest)
        {
            var result = await _categoryService.Add(categoryRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Update an Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryUpdateRequest">CategoryUpdate</param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<ActionResult<Response<CategoryDTO_>>> Update(Guid id, CategoryUpdateRequest categoryUpdateRequest)
        {
            var result = await _categoryService.Update(id, categoryUpdateRequest);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Delete an Category
        /// </summary>
        /// <param name="Id">Id Category</param>
        /// <returns></returns>
        [HttpDelete()]
        public async Task<ActionResult<BaseResponse>> Delete(Guid Id)
        {
            var result = await _categoryService.remove(Id);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
