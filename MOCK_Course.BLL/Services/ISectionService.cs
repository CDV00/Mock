﻿using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface ISectionService
    {
        Task<Responses<SectionResponse>> GetAll(Guid courseId);
        Task<Response<SectionResponse>> Add(SectionRequest sectionRequest);
        Task<BaseResponse> Remove(Guid idSection);
        Task<Response<SectionResponse>> Update(SectionUpdateRequest sectionRequest);
    }
}
