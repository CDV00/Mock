using Contracts;
using Course.DAL.Repositories.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CourseAPI.ActionFilters
{
    public class ValidationCourseExistAttribut : IAsyncActionFilter
    {
        private readonly ICourseRepository _repository;
        private readonly ILoggerManager _logger;
        public ValidationCourseExistAttribut(ICourseRepository repository,
       ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
       ActionExecutionDelegate next)
        {
            //var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var course = await _repository.GetByIdAsync(id);
            if (course == null)
            {
                _logger.LogInfo($"course with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundObjectResult($"Not found course with id: {id}");
            }
            else
            {
                context.HttpContext.Items.Add("course", course);
                await next();
            }
        }
    }
}
