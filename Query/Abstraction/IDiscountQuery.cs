using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface IDiscountQuery : IQuery<Discount>
    {
        IDiscountQuery FilterByUserId(Guid UserId);
        IDiscountQuery IncludeCourses();
    }
}