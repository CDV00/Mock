using Contracts;
using Course.DAL.Repositories.Abstraction;
using CourseAPI.ErrorModel;
using Entities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CourseAPI.ActionFilters
{
    public class ValidationCourseExistAttribut : IAsyncActionFilter
    {
        private readonly ICourseRepository _repository;
        private readonly ILoggerManagerService _logger;
        public ValidationCourseExistAttribut(ICourseRepository repository,
       ILoggerManagerService logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
       ActionExecutionDelegate next)
        {
            var id = (Guid)context.ActionArguments[BaseEntityConstant.Id];
            var course = await _repository.GetByIdAsync(id);
            if (course == null)
            {
                _logger.LogInfo($"course with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundObjectResult(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Not found course with id: {id}"
                });
            }
            else
            {
                context.HttpContext.Items.Add(CourseConstant.Name, course);
                await next();
            }
        }
    }
}
