using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;

namespace Course.BLL.Services.Abstraction
{
    public interface IAssignmentService
    {
        Task<PagedList<AssignmentDTO>> GetAll(Guid sectionId, AssignmentParameters assignmentParameters);
        Task<Response<AssignmentDTO>> Add(AssignmentForCreateRequest assignmentForCreateRequest);
    }
}
