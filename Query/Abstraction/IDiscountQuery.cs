using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface IDiscountQuery : IQuery<Discount>
    {
        IDiscountQuery checkDate(Discount discount);
        IDiscountQuery CheckDuringDate();
        IDiscountQuery FilterByCourseId(Guid courseId);
        IDiscountQuery FilterByUserId(Guid UserId);
        IDiscountQuery IncludeCourses();
        IDiscountQuery IncludeOrderItem();
    }
}