using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;
using Course.DAL.DTOs;

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
        //Task<Responses<float>> GetRate(Guid courseId);
        //Task<Response<float>> GetDetaiRate(Guid courseId, float rating);
        Task<Response<RatingDetailDTO>> GetDetaiRate(Guid courseId);
        Task<BaseResponse> IsCourseReview(Guid userId, Guid courseId);
    }
}
