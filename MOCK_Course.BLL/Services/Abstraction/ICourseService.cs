﻿using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Entities.Responses;
using Entities.ParameterRequest;
using Microsoft.AspNetCore.JsonPatch;

namespace Course.BLL.Services.Abstraction
{
    public interface ICourseService
    {
        Task<ApiBaseResponse> Add(Guid userId, CourseForCreateRequest courseRequest);
        Task<ApiBaseResponse> Update(Guid id, CourseForUpdateRequest courseRequest, Guid userId);
        Task<ApiBaseResponse> Remove(Guid id, Guid userId);
        Task<Response<int>> GetTotalCourseOfUser(Guid userId);
        //Task<Responses<CourseDTO>> GetAllMyCoures(Guid userId);
        Task<ApiBaseResponse> GetAllMyCoures(CourseParameters parameter, Guid userId);
        Task<ApiBaseResponse> UpcomingCourse(CourseParameters parameter, Guid userId);
        Task<ApiBaseResponse> GetAllMyPurchase(CourseParameters parameter, Guid userId);
        Task<ApiBaseResponse> GetAllCourses(CourseParameters courseParameter, Guid? userId);
        Task<ApiBaseResponse> UpdateStatus(CourseStatusUpdateRequest courseStatusUpdateRequest);
        //Task<Responses<CourseDTO>> UpcomingCourse(Guid userId);
        Task<ApiBaseResponse> GetDetail(Guid id, Guid? userId);
        Task<ApiBaseResponse> GetAllMyLearning(CourseParameters parameter, Guid userId);
        Task<ApiBaseResponse> UpdatePatch(Guid id, JsonPatchDocument<CourseForUpdateRequest> coursePatch, Guid userId);
    }
}
