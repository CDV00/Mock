using Contracts;
using Course.BLL.Requests;
using Course.DAL.Repositories.Abstraction;
using CourseAPI.ErrorModel;
using Entities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CourseAPI.ActionFilters
{
    public class ValidationDateTimeForDiscountAttribute : IAsyncActionFilter
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILoggerManager _logger;
        public ValidationDateTimeForDiscountAttribute(
       ILoggerManager logger, IDiscountRepository discountRepository)
        {
            _logger = logger;
            _discountRepository = discountRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
       ActionExecutionDelegate next)
        {
            var discount = (context.ActionArguments[DiscountConstant.Name] as dynamic);

            int discounts = await _discountRepository.BuildQuery()
                                                     .FilterByCourseId(discount.courseId)
                                                     .CheckDateDiscountExist(discount.StartDate, discount.EndDate)
                                                     .CountAsync();

            if (discounts > 0)
            {
                _logger.LogInfo($"Already have discount available during this time");
                context.Result =
                        new UnprocessableEntityObjectResult(new ErrorDetails
                        {
                            StatusCode = 1013,
                            Message = $"Already have discount with start date: {discount.StartDate} and end day: {discount.EndDate}"
                        });
            }
            else
            {
                await next();
            }
        }
    }
}
