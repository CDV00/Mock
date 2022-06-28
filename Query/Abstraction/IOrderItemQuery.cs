using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Query.Abstraction
{
    public interface IOrderItemQuery : IQuery<OrderItem>
    {
        IOrderItemQuery FilterByCourseId(Guid CourseId);
        IOrderItemQuery FilterByUserId(Guid userId);
        IOrderItemQuery FilterByUserIdInstructor(Guid userId);
        IOrderItemQuery FilterEndtDate(DateTime? CreateAt);
        IOrderItemQuery FilterStartDate(DateTime? CreateAt);
        IOrderItemQuery IncludeCourse();
    }
}