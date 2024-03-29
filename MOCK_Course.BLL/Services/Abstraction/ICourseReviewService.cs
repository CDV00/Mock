﻿using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Course.BLL.Share.RequestFeatures;
using Entities.ParameterRequest;
using Entities.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface ICourseReviewService
    {
        Task<ApiBaseResponse> GetTotalReviewOfUser(Guid userId);
        Task<Response<float>> GetAVGRatinng(Guid? courseId, Guid? userId);
        Task<Response<List<float>>> GetDetaiRate(Guid? courseId, Guid? userId);
        Task<Response<BaseResponse>> IsCourseReview(Guid userId, Guid courseId);
        Task<Response<CourseReviewDTO>> CheckUserCourseReview(Guid userId, Guid courseId);
        Task<Response<int>> GetTotalReviewOfCourse(Guid courseId);
        Task<Response<int>> GetTotalReviewOfInstructor(Guid userId);
        Task<PagedList<CourseReviewDTO>> GetAllCourseReviewOfIntructor(CourseReviewParameters courseReviewParameters, Guid userId);
        Task<PagedList<CourseReviewDTO>> GetAllCourseReviewOfUser(Guid userId, CourseReviewParameters courseReviewParameters);
        Task<Response<float>> GetAVGRatinngOfIntructor(Guid userId);
        Task<Response<List<float>>> GetDetaiRateOfIntructor(Guid userId);
        Task<ApiBaseResponse> GetAll(Guid courseId, CourseReviewParameters parameter);
        Task<ApiBaseResponse> Add(CourseReviewRequest courseReviewRequest);
        Task<ApiBaseResponse> Update(Guid id, CourseReviewUpdateRequest courseReviewUpdateRequest, Guid userId);
        Task<ApiBaseResponse> Delete(Guid id, Guid userId);
    }
}
