﻿using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Share.RequestFeatures;
using Entities.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface ISavedCoursesService
    {
        Task<ApiBaseResponse> Add(Guid userId, Guid courseId);
        //Task<BaseResponse> Remove(Guid courseId, Guid userId);
        Task<BaseResponse> RemoveAll(Guid userId);
        Task<Response<bool>> IsSaveCourses(Guid userId, Guid courseId);

        //Task<BaseResponse> Remove(Guid userId);
        Task<bool> IsSavedCourse(Guid userId, Guid courseId);
        Task<ApiBaseResponse> GetAll(Guid userId, SavedCoursesParameters parameters);
        Task<ApiBaseResponse> Remove(Guid courseId, Guid userId);
    }
}
