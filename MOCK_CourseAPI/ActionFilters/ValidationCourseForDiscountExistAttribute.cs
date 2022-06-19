using Contracts;
using Course.BLL.Requests;
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
    public class ValidationCourseForDiscountExistAttribute : IAsyncActionFilter
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly ILoggerManager _logger;
        public ValidationCourseForDiscountExistAttribute(ICourseRepository courseRepository,
       ILoggerManager logger, IDiscountRepository discountRepository)
        {
            _logger = logger;
            _courseRepository = courseRepository;
            _discountRepository = discountRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
       ActionExecutionDelegate next)
        {
            Guid courseId = (context.ActionArguments[DiscountConstant.Name] as dynamic).CourseId;

            var course = await _courseRepository.BuildQuery()
                                                .FilterById(courseId)
                                                .FilterIsActive(true)
                                                .FilterByApprove()
                                                .AsSelectorAsync(c => c);

            if (course == null)
            {
                _logger.LogInfo($"course with id: {courseId} doesn't exist in the database.");

                context.Result = new NotFoundObjectResult(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Not found course with id: {courseId}"
                });
                return;
            }
            context.HttpContext.Items.Add(CourseConstant.Name, course);
            await next();
        }
    }
}
