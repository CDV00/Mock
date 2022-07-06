using Course.BLL.Responses;
using Course.DAL.Models;
using Entities.ParameterRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Queries.Abstraction
{
    public interface IOrderQuery : IQuery<Order>
    {
        Task<int> CountGroupByCreate(OrderParameters parameter);
        IOrderQuery FilterByCourseId(Guid CourseId);
        IOrderQuery FilterByCreateAt(DateTime CreatedAt);
        IOrderQuery FilterById(Guid Id);
        IOrderQuery FilterByUserId(Guid userId);
        IOrderQuery FilterByUserIdInstructor(Guid userId);
        IOrderQuery FilterEndtDate(DateTime? CreateAt);
        IOrderQuery FilterIsActive(bool? isActice);
        IOrderQuery FilterStartDate(DateTime? CreateAt);
        Task<List<EarningDTO>> GetGroupByCreate(OrderParameters parameter);
        IOrderQuery IncludeCourse();
        IOrderQuery IncludeDiscount();
        IOrderQuery IncludeUser();
    }
}