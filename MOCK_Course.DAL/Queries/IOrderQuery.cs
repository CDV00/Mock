using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Queries
{
    public interface IOrderQuery : IQuery<Order>
    {
        IOrderQuery FilterByCourseId(Guid CourseId);
        IOrderQuery FilterIsActive(bool? isActice);
    }
}