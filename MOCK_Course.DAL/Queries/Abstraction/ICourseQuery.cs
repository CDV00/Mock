using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Queries
{
    public interface ICourseQuery : IQuery<Courses>
    {
        ICourseQuery FilterByUserId(Guid userId);
        ICourseQuery FilterIsActive(bool? isActice);
        Task<List<Courses>> GetAllByUserIdAsync(Guid userId);
        ICourseQuery FilterById(Guid Id);
        ICourseQuery IncludeUser();
        ICourseQuery IncludeLanguage();
        ICourseQuery IncludeCategory();
        ICourseQuery IncludeSection();
        ICourseQuery FilterByOrderd(Guid userId);
    }
}