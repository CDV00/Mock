using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface IOrderQuery : IQuery<Order>
    {
        IOrderQuery FilterByCourseId(Guid CourseId);
        IOrderQuery FilterByCreateAt(DateTime CreatedAt);
        IOrderQuery FilterById(Guid Id);
        IOrderQuery FilterByUserId(Guid userId);
        IOrderQuery FilterByUserIdInstructor(Guid userId);
        IOrderQuery FilterEndtDate(DateTime? CreateAt);
        IOrderQuery FilterIsActive(bool? isActice);
        IOrderQuery FilterStartDate(DateTime? CreateAt);
        IOrderQuery GroupByCreateAt();
        IOrderQuery IncludeCourse();
        IOrderQuery IncludeDiscount();
        IOrderQuery IncludeUser();
    }
}