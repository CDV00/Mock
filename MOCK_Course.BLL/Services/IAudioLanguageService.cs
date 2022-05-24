using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responses;

namespace Course.BLL.Services
{
    public interface IAudioLanguageService
    {
        Task<Response<AudioLanguageCreateResponse>> Add(AudioLanguageCreateRequest audioLanguageRequest, System.Guid courseId);
    }
}
