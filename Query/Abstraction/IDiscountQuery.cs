using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface IDiscountQuery : IQuery<Discount>
    {
        IDiscountQuery CheckDateDiscountExist(DateTime StartDate, DateTime EndDate);
        IDiscountQuery CheckDuringDate();
        IDiscountQuery FilterByCourseId(Guid courseId);
        IDiscountQuery FilterById(Guid id);
        IDiscountQuery FilterByUserId(Guid UserId);
        IDiscountQuery IncludeCourses();
        IDiscountQuery IncludeOrderItem();
    }
}