using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Responsesnamespace;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface ICourseReviewService
    {
        //Task<Responses<CourseReviewResponse>> GetAll();
        Task<Responses<CourseReviewResponse>> GetAll(Guid courseId);
        Task<Response<CourseReviewResponse>> Add( CourseReviewRequest courseReviewRequest);
        Task<Response<CourseReviewResponse>> Update(CourseReviewUpdateRequest courseReviewUpdateRequest);
        Task<BaseResponse> Delete(Guid id);
    }
}
