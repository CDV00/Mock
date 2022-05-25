using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface IEnrollmentService
    {
        Task<Responses<EnrollmentResponse>> GetAll(Guid userId);
        Task<Response<BaseResponse>> Add(EnrollmentRequest enrollmentRequest);
        Task<BaseResponse> Remove(Guid userId);
        Task<Response<EnrollmentResponse>> Update(EnrollmentUpdateRequest enrollmentUpdateRequest);
    }
}
