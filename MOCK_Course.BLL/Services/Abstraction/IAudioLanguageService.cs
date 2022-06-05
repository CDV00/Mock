using System;
using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;

namespace Course.BLL.Services.Abstraction
{
    public interface IAudioLanguageService

    {
        Task<Responses<AudioLanguageDTO>> GetAll();
        Task<Response<AudioLanguageDTO>> Add(AudioLanguageForCreateRequest audioLanguageRequest, Guid courseId);
    }
}
