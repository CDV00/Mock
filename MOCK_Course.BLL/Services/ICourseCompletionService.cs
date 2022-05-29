﻿using System;
using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.DTO;

namespace Course.BLL.Services
{
    public interface ICourseCompletionService
    {
        Task<BaseResponse> Add(Guid userId, CourseCompletionRequest courseCompletionRequest);
    }
}
