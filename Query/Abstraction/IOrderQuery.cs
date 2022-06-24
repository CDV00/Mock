using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface IOrderQuery : IQuery<Order>
    {
        IOrderQuery FilterByCourseId(Guid CourseId);
        IOrderQuery FilterById(Guid Id);
        IOrderQuery FilterByUserId(Guid userId);
        IOrderQuery FilterIsActive(bool? isActice);
        IOrderQuery IncludeCourse();
        IOrderQuery IncludeDiscount();
        IOrderQuery IncludeUser();
    }
}