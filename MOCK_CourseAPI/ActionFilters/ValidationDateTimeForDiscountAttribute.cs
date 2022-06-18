using Contracts;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Microsoft.AspNetCore.Http;
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
            var courseId = (Guid)context.ActionArguments["courseId"];
            var discount = context.ActionArguments["discount"] as DiscountForCreateRequest;
            if (discount == null)
                discount = context.ActionArguments["discount"] as DiscountForUpdateRequest;

            int discounts = await _discountRepository.BuildQuery()
                                                     .FilterByCourseId(courseId)
                                                     .CheckDateDiscountExist(discount.StartDate, discount.EndDate)
                                                     .CountAsync();

            if (discounts > 0)
            {
                _logger.LogInfo($"Already have discount available during this time");
                context.Result = // new StatusCodeResult(1001,"Already have discount available during this time");
                new ObjectResult("Already have discount available during this time")
                {
                    StatusCode = 2078,
                };
            }
            else
            {
                await next();
            }
        }
    }
}
