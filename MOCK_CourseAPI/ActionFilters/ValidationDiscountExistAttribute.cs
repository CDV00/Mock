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
    public class ValidationDiscountExistAttribute : IAsyncActionFilter
    {
        private readonly IDiscountRepository _repository;
        private readonly ILoggerManagerService _logger;
        public ValidationDiscountExistAttribute(IDiscountRepository repository,
       ILoggerManagerService logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
       ActionExecutionDelegate next)
        {
            var id = (Guid)context.ActionArguments[BaseEntityConstant.Id];
            var courseId = (Guid)context.ActionArguments[CourseConstant.courseId];
            var Discount = await _repository.GetByIdAsync(courseId, id);
            if (Discount == null)
            {
                _logger.LogInfo($"Discount with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundObjectResult(
                    new ErrorDetails
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Not found discount with id: {id}."
                    });
            }
            else
            {
                context.HttpContext.Items.Add(DiscountConstant.Name, Discount);
                await next();
            }
        }
    }
}
