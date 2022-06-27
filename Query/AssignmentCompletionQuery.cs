using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Query.Abstraction;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Query
{
    public class AssignmentCompletionQuery : QueryBase<AssignmentCompletion>, IAssignmentCompletionQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public AssignmentCompletionQuery(IQueryable<AssignmentCompletion> assignmentCompletions, AppDbContext dbContext) : base(assignmentCompletions)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }

        public IAssignmentCompletionQuery FilterByUser(Guid userId)
        {
            Query = Query.Where(type => type.UserId == userId);
            return this;
        }
        public IAssignmentCompletionQuery FilterByAssignment(Guid asignmentId)
        {
            Query = Query.Where(type => type.AssignmentId == asignmentId);
            return this;
        }
    }
}
