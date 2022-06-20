using Contracts;
using Course.DAL.Repositories.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CourseAPI.ActionFilters
{
    public class ValidationDiscountExistAttribute : IAsyncActionFilter
    {
        private readonly IDiscountRepository _repository;
        private readonly ILoggerManager _logger;
        public ValidationDiscountExistAttribute(IDiscountRepository repository,
       ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
       ActionExecutionDelegate next)
        {
            var id = (Guid)context.ActionArguments["id"];
            var courseId = (Guid)context.ActionArguments["courseId"];
            var Discount = await _repository.GetByIdAsync(courseId, id);
            if (Discount == null)
            {
                _logger.LogInfo($"Discount with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundObjectResult($"Not found discount with id: {id}.");
            }
            else
            {
                context.HttpContext.Items.Add("Discount", Discount);
                await next();
            }
        }
    }
}
