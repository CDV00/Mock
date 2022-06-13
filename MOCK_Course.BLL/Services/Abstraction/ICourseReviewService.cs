using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Course.BLL.Share.RequestFeatures;

namespace Course.BLL.Services.Abstraction
{
    public interface ICourseReviewService
    {
        //Task<Responses<CourseReviewDTO>> GetAll(Guid courseId);
        Task<PagedList<CourseReviewDTO>> GetAll(Guid courseId, CourseReviewParameters courseReviewParameters);
        //Task<Responses<CourseReviewDTO>> Getall(Guid courseId);
        Task<Response<CourseReviewDTO>> Add(CourseReviewRequest courseReviewRequest);
        Task<Response<CourseReviewDTO>> Update(Guid id, CourseReviewUpdateRequest courseReviewUpdateRequest);
        Task<BaseResponse> Delete(Guid id);
        Task<Response<int>> GetTotalReviewOfUser(Guid userId);
        Task<Response<float>> GetAVGRatinng(Guid? courseId, Guid? userId);
        Task<Response<List<float>>> GetDetaiRate(Guid? courseId, Guid? userId);
        Task<BaseResponse> IsCourseReview(Guid userId, Guid courseId);
        Task<Response<CourseReviewDTO>> CheckUserCourseReview(Guid userId, Guid courseId);
        Task<Response<int>> GetTotalReviewOfCourse(Guid courseId);
        Task<Response<int>> GetTotalReviewOfInstructor(Guid userId);
        Task<PagedList<CourseReviewDTO>> GetAllCourseReviewOfIntructor(RequestParameters requestParameters, Guid userId);
        Task<PagedList<CourseReviewDTO>> GetAllCourseReviewOfUser(Guid userId, CourseReviewParameters courseReviewParameters);
    }
}
