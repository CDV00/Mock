using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Course.BLL.Services.Abstraction
{
    public interface ICourseReviewService
    {
        Task<Responses<CourseReviewDTO>> GetAll(Guid courseId);
        Task<Response<CourseReviewDTO>> Add(CourseReviewRequest courseReviewRequest);
        Task<Response<CourseReviewDTO>> Update(Guid id, CourseReviewUpdateRequest courseReviewUpdateRequest);
        Task<BaseResponse> Delete(Guid id);
        Task<Response<int>> GetTotal(Guid userId);
        Task<Response<float>> GetAVGRatinng(Guid courseId);
        Task<Response<List<float>>> GetDetaiRate(Guid courseId);
        Task<BaseResponse> IsCourseReview(Guid userId, Guid courseId);
    }
}
