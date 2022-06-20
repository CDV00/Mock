using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Queries
{
    public class AssignmentQuery : QueryBase<Assignment>, IAssignmentQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public AssignmentQuery(IQueryable<Assignment> assignmentQuery, AppDbContext dbContext) : base(assignmentQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }

        public IAssignmentQuery FilterBySectionId(Guid? SectionId)
        {
            if (SectionId == null)
                return this;

            Query = Query.Where(type => type.SectionId == SectionId);
            return this;
        }
        public IAssignmentQuery FilterByKeyword(string Keyword)
        {
            if (string.IsNullOrWhiteSpace(Keyword))
                return this;

            var KEYWORD = Keyword.ToUpper();
            Query = Query.Where(c => c.Title.ToUpper().Contains(KEYWORD) 
                                || c.Content.ToUpper().Contains(KEYWORD));
            return this;
        }
        public IAssignmentQuery IncludeSection()
        {
            Query.Include(c => c.Section).Load();
            return this;
        }
        public IAssignmentQuery IncludeAttachment()
        {
            Query.Include(c => c.Attachments).Load();
            return this;
        }

    }
}
