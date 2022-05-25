﻿using System.Threading.Tasks;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface ISectionService
    {
        Task<Responses<SectionResponse>> GetAll(Guid courseId);
        Task<Response<SectionResponse>> Add(Guid CourseId, SectionCreateRequest sectionRequest);
        Task<BaseResponse> Remove(Guid idSection);
        Task<Response<SectionResponse>> Update(Guid id, SectionUpdateRequest sectionRequest);
    }
}
