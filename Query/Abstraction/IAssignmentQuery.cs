using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Queries.Abstraction
{
    public interface IAssignmentQuery : IQuery<Assignment>
    {
        IAssignmentQuery FilterBySectionId(Guid? SectionId);
        IAssignmentQuery FilterByKeyword(string Keyword);
        IAssignmentQuery IncludeSection();
    }
}