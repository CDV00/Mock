﻿using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;

namespace Course.BLL.Services
{
    public interface ICourseService
    {
        Task<Responses<CoursesCardDTO>> GetAll();
        Task<Responses<CoursesCardDTO>> GetMyPurchase(Guid id);
        Task<Response<CourseDTO>> GetForPost(Guid id);
        Task<Response<CourseDTO>> Add(Guid userId, CourseForCreateRequest courseRequest);
        Task<Response<CourseDTO>> Update(Guid id, CourseForUpdateRequest courseRequest);
        Task<BaseResponse> Remove(Guid id);
        Task<Response<int>> GetTotal(Guid userId);
    }
}
