using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using System;
using Course.BLL.DTO;

namespace Course.BLL.Services
{
    public interface IEnrollmentService
    {
        //Task<Responses<EnrollmentResponse>> GetAll(Guid userId);
        Task<BaseResponse> IsEnrollmented(EnrollmentRequest enrollmentRequest);
        Task<BaseResponse> Add(EnrollmentRequest enrollmentRequest);
        //Task<BaseResponse> Remove(Guid userId);
        //Task<Response<EnrollmentResponse>> Update(EnrollmentUpdateRequest enrollmentUpdateRequest);
    }
}
