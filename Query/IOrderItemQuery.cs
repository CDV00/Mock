using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Queries
{
    public interface IOrderItemQuery : IQuery<OrderItem>
    {
        IOrderItemQuery FilterByCourseId(Guid CourseId);
        IOrderItemQuery FilterByUserId(Guid userId);
    }
}