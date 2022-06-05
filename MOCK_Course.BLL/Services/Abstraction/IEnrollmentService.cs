using System.Threading.Tasks;
using System;
using Course.BLL.DTO;
using Course.BLL.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface IEnrollmentService
    {
        Task<Response<EnrollmentDTO>> Add(Guid userId, Guid CourseId);
        Task<Response<int>> GetTotal(Guid userId);
        Task<Response<EnrollmentDTO>> IsEnrollment(Guid userId, Guid courseId);
    }
}
