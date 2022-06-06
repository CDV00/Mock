using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface IOrderQuery : IQuery<Order>
    {
        IOrderQuery FilterByCourseId(Guid CourseId);
        IOrderQuery FilterByUserId(Guid userId);
        IOrderQuery FilterIsActive(bool? isActice);
    }
}