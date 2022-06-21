﻿using System.Threading.Tasks;
using System;
using Course.BLL.DTO;
using Course.BLL.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface IEnrollmentService
    {
        Task<Response<EnrollmentDTO>> Add(Guid userId, Guid CourseId);
        Task<Response<int>> GetTotalEnrollOfUser(Guid userId);
        Task<Response<int>> GetTotalEnrollOfCourse(Guid courseId);
        Task<Response<EnrollmentDTO>> IsEnrollment(Guid userId, Guid courseId);
        Task<Response<int>> GetTotalEnrollOfInstructor(Guid userId);
        Task<Responses<EnrollmentDTO>> GetAll(Guid? userId);
    }
}
