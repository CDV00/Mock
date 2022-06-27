using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Query.Abstraction
{
    public interface IAssignmentCompletionQuery : IQuery<AssignmentCompletion>
    {
        IAssignmentCompletionQuery FilterByAssignment(Guid asignmentId);
        IAssignmentCompletionQuery FilterByUser(Guid userId);
    }
}