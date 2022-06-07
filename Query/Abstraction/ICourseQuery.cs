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
        Task<Courses> GetById(Guid Id);
        ICourseQuery IncludeUser();
        ICourseQuery IncludeLanguage();
        ICourseQuery IncludeCategory();
        ICourseQuery IncludeSection();
        ICourseQuery FilterByOrderd(Guid userId);
        ICourseQuery FilterByCategoryId(Guid? categoryId);
        ICourseQuery FilterByAudioLanguageIds(List<Guid?> AudioLanguageIds);
        ICourseQuery FilterByCloseCaptionIds(List<Guid?> closeCaptionIds);
        ICourseQuery FilterByLevelIds(List<Guid?> levelIds);
        ICourseQuery FilterByKeyword(string Keyword);
        ICourseQuery ApplySort(string orderby);
    }
}