﻿using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Course.DAL.DTOs;
using Course.BLL.Services.Abstraction;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelService;
        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }
        /// <summary>
        /// Get All Level
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Responses<LevelDTO>>> GetAll()
        {
            var result = await _levelService.GetAll();
            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
