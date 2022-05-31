using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using System;
using Course.BLL.DTO;

namespace Course.BLL.Services
{
    public interface IEnrollmentService
    {
        Task<BaseResponse> Add(Guid userId, EnrollmentRequest enrollmentRequest);
        Task<Response<int>> GetTotal(Guid userId);
    }
}
