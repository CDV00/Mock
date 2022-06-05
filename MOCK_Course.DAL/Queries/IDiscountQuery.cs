using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.Queries
{
    public interface IDiscountQuery : IQuery<Discount>
    {
        IDiscountQuery FilterByUserId(Guid UserId);
        IDiscountQuery IncludeCourses();
    }
}