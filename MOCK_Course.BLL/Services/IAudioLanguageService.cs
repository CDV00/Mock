using System;
using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;

namespace Course.BLL.Services
{
    public interface IAudioLanguageService
    {
        Task<Response<AudioLanguageDTO>> Add(AudioLanguageForCreateRequest audioLanguageRequest, Guid courseId);
    }
}
