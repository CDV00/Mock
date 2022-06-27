using Course.BLL.DTO;
using Course.BLL.Requests;
using Entities.DTOs;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services.Abstraction
{
    public interface IAssignmentCompletionService
    {
        Task<Response<AssignmentCompletionDTO>> Add(Guid userId, AssignmentCompletionRequest assignmentCompletionRequest);
        Task<BaseResponse> IsCompletion(Guid userId, Guid assignmentId);
    }
}