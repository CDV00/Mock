﻿using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using Course.BLL.Responses;
using Microsoft.AspNetCore.Authorization;
using CourseAPI.Extensions.ControllerBase;
using Course.BLL.Services.Abstraction;
using System.Text.Json;
using CourseAPI.Presentation.Controllers;
using Entities.Responses;
using Course.BLL.Share.RequestFeatures;
using Entities.Extension;
using Entities.Constants;
using Entities.ParameterRequest;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using Contracts;
using CourseAPI.ActionFilters;
using CourseAPI.Utility;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using Entities.LinkModels;
using Entities.Models;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Instructor, Student")]
    public class CourseController : ApiControllerBase
    {
        private readonly ICourseService _coursesService;
        private readonly IMapper _mapper;
        private readonly IDataShaper<CourseDTO> _dataShaper;
        private readonly CourseLinks _courseLinks;
        private readonly LinkGenerator _linkGenerator;
        public CourseController(ICourseService coursesService,
                                IMapper mapper,
                                IDataShaper<CourseDTO> dataShaper,
                                CourseLinks courseLinks,
                                LinkGenerator linkGenerator)
        {
            _coursesService = coursesService;
            _mapper = mapper;
            _dataShaper = dataShaper;
            _courseLinks = courseLinks;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Get all course with paging and filter
        /// </summary>
        /// <remarks>
        /// ## Remark
        /// StatusOfUser
        /// - 0 :Enrolled
        /// - 1 :Saved
        /// - 2 :AddedCart
        /// - 3 :Purchased
        /// </remarks>
        /// <param name="parameters"></param>
        /// <returns>List of course</returns>
        [HttpGet("Get-all-course", Name = "getallcourses")]
        //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [AllowAnonymous]
        public async Task<ActionResult<ApiOkResponses<CourseDTO>>> GetAllCourses([FromQuery] CourseParameters parameters)
        {
            Guid? userId = (User.GetUserId() == Guid.Empty) ? null : User.GetUserId();

            var result = await _coursesService.GetAllCourses(parameters, userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            var coursePagedList = result.GetResult<PagedList<CourseDTO>>();

            Response.Headers.Add(SystemConstant.PagedHeader,
                                 JsonSerializer.Serialize(coursePagedList.MetaData));

            return Ok(new ApiOkResponses<CourseDTO>(coursePagedList));
        }


        /// <summary>
        /// Get Detail course Include User, Category, Language, Levels 
        /// </summary>
        /// <returns>an course</returns>
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<ApiOkResponse<CourseDTO>>> Get(Guid id)
        {
            Guid? userId = (User.GetUserId() == Guid.Empty) ? null : User.GetUserId();

            var result = await _coursesService.GetDetail(id, userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Create new course  
        /// </summary>
        /// <remarks>
        /// ## Remark
        /// Type Question:
        /// - 0: SingleChoice
        /// - 1: MultipleChoice
        /// - 2: SingleLineText
        /// - 3: MiltiText
        /// Category: 9e47da02-3d3e-248d-a207-d53908753582
        /// </remarks>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<ActionResult<ApiOkResponse<CourseDTO>>> Create([FromBody] CourseForCreateRequest course)
        {
            var userId = User.GetUserId();
            var result = await _coursesService.Add(userId, course);
            if (!result.IsSuccess)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Update course
        /// </summary>
        /// <remarks>
        /// ## Remarks
        /// When add new records in course like section, quiz, lecture,...
        /// You must Generate Id Guid type, it will save id you pass in. if you want deleted it, just set IsDeleted = true
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="CoursesUpdateRequest"></param>
        /// <returns>an course</returns>
        [HttpPut()]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<ActionResult<ApiOkResponse<CourseDTO>>> Update(Guid id, CourseForUpdateRequest CoursesUpdateRequest)
        {
            var userId = User.GetUserId();

            var result = await _coursesService.Update(id, CoursesUpdateRequest, userId);
            if (result.IsSuccess == false)
                return ProcessError(result);

            return Ok(result);
        }

        [HttpPut("Partial-Update")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<ActionResult<ApiOkResponse<CourseDTO>>> PartialUpdate(Guid id, [FromBody] JsonPatchDocument<CourseForUpdateRequest> patchCourse)
        {
            var userId = User.GetUserId();

            var result = await _coursesService.UpdatePatch(id, patchCourse, userId);
            if (result.IsSuccess == false)
                return ProcessError(result);

            return Ok(result);
        }


        /// <summary>
        /// Delete an Courses by Id
        /// </summary>
        /// <param name="id">Id Courses</param>
        /// <returns>true or false</returns>
        [HttpDelete()]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<ActionResult<ApiBaseResponse>> Delete(Guid id)
        {
            var userId = User.GetUserId();
            var result = await _coursesService.Remove(id, userId);
            if (result.IsSuccess == false)
                return ProcessError(result);

            return Ok(result);
        }
        /// <summary>
        /// Get Total Course of Instructor 
        /// </summary>
        /// <returns>total course as Integer type</returns>
        [HttpGet("Get-total-courses")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<int>>> GetTotal(Guid UserId)
        {
            var result = await _coursesService.GetTotalCourseOfUser(UserId);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Get all My Course of Instructor
        /// </summary>
        /// <returns>List Courses</returns>
        [HttpGet("Get-all-my-course")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<ActionResult<ApiOkResponse<CourseDTO>>> GetAllMyCoures([FromQuery] CourseParameters parameters)
        {
            var userId = User.GetUserId();

            var result = await _coursesService.GetAllMyCoures(parameters, userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            var coursePagedList = result.GetResult<PagedList<CourseDTO>>();

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(coursePagedList.MetaData));

            return Ok(result);
        }

        /// <summary>
        /// Get all Course, User already purchased
        /// </summary>
        /// <returns>List Courses</returns>
        [HttpGet("Get-all-my-purchased")]
        public async Task<ActionResult<ApiOkResponse<CourseDTO>>> GetAllMyPurchase([FromQuery] CourseParameters parameters)
        {
            var userId = User.GetUserId();

            var result = await _coursesService.GetAllMyPurchase(parameters, userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            var coursePagedList = result.GetResult<PagedList<CourseDTO>>();

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(coursePagedList.MetaData));

            return Ok(result);
        }

        [HttpGet("Get-all-up-coming-Course")]
        public async Task<ActionResult<ApiOkResponse<CourseDTO>>> GetUpcomingCourse([FromQuery] CourseParameters parameters)
        {
            var userId = User.GetUserId();

            var result = await _coursesService.UpcomingCourse(parameters, userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            var coursePagedList = result.GetResult<PagedList<CourseDTO>>();

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(coursePagedList.MetaData));

            return Ok(new Responses<CourseDTO>(true, coursePagedList));
        }

        /// <summary>
        /// Get All My learning
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet("Get-all-my-learning")]
        public async Task<ActionResult<ApiOkResponse<CourseDTO>>> GetAllMyLearning([FromQuery] CourseParameters parameters)
        {
            var userId = User.GetUserId();

            var result = await _coursesService.GetAllMyLearning(parameters, userId);
            if (!result.IsSuccess)
                return ProcessError(result);

            var coursePagedList = result.GetResult<PagedList<CourseDTO>>();

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(coursePagedList.MetaData));

            return Ok(result);
        }

        /// <summary>
        /// Update Status Course
        /// </summary>
        /// <param name="courseStatusUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut("update-status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiOkResponse<CourseDTO>>> UpdateStatus(CourseStatusUpdateRequest courseStatusUpdateRequest)
        {
            var result = await _coursesService.UpdateStatus(courseStatusUpdateRequest);
            if (result.IsSuccess == false)
                return ProcessError(result);

            return Ok(result);
        }



























        //[HttpGet("is-courses-free/{courseId}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<Responses<CourseDTO>>> IsCoursesFree(Guid courseId)
        //{
        //    var result = await _coursesService.IsFree(courseId);
        //    if (result.IsSuccess == false && result.Message != null)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
        /// <summary>
        /// Erroring
        /// Comming soon...
        /// </summary>
        /// <param name="cousrsePagingRequest"></param>
        /// <returns></returns>
        //[HttpGet("Get-course-paing")]
        //[AllowAnonymous]
        //public async Task<ActionResult<Responses<CousrsePagingDTO>>> GetCoursePaings([FromQuery] CousrsePagingRequest cousrsePagingRequest)
        //{
        //    var result = await _coursesService.GetCoursePaing(cousrsePagingRequest);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}

        /// <summary>
        /// Erroring
        /// Comming soon...
        /// </summary>
        /// <param name="cousrsePagingRequest"></param>
        /// <returns></returns>
        //[HttpPost("Search/{key}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<Responses<CousrsePagingDTO>>> GetByFilteringCousrse(string key)
        //{
        //    var result = await _coursesService.GetByFilteringCousrse(key);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}

        /// <summary>
        /// Get all course! This API is old, need use Get-all-course to paging and filter
        /// </summary>
        /// <returns>List of course</returns>
        //[HttpGet("Get-all")]
        //[AllowAnonymous]
        //public async Task<ActionResult<Responses<CourseDTO>>> GetAll()
        //{

        //    Guid? userId = (User.GetUserId() == Guid.Empty) ? null : User.GetUserId();
        //    var result = await _coursesService.GetAll(userId);
        //    if (result.IsSuccess == false)
        //        return BadRequest(result);
        //    return Ok(result);
        //}
    }
}
