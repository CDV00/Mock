﻿using System;
using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Responsesnamespace;

namespace Course.BLL.Services
{
    public interface IAudioLanguageService
    {
        Task<Response<AudioLanguageCreateResponse>> Add(AudioLanguageCreateRequest audioLanguageRequest, Guid courseId);

        Task<BaseResponse> RemoveAll(Guid courseId);
    }
}
