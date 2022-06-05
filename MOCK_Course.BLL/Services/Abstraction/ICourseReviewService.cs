using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services.Abstraction
{
    public interface ICourseReviewService
    {
        Task<Responses<CourseReviewDTO>> GetAll(Guid courseId);
        Task<Response<CourseReviewDTO>> Add(CourseReviewRequest courseReviewRequest);
        Task<Response<CourseReviewDTO>> Update(Guid id, CourseReviewUpdateRequest courseReviewUpdateRequest);
        Task<BaseResponse> Delete(Guid id);
        Task<Response<int>> GetTotal(Guid userId);
    }
}
