using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface ICourseReviewService
    {
        //Task<Responses<CourseReviewResponse>> GetAll();
        Task<Responses<CourseReviewResponse>> GetAll(Guid courseId);
        Task<Response<CourseReviewResponse>> Add(CourseReviewRequest courseReviewRequest);
        Task<Response<CourseReviewResponse>> Update(Guid id, CourseReviewUpdateRequest courseReviewUpdateRequest);
        Task<BaseResponse> Delete(Guid id);
    }
}
