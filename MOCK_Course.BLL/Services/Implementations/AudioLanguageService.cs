using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.DAL.Models;
using Course.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
{
    public class AudioLanguageService : IAudioLanguageService
    {
        private readonly IAudioLanguageRepository _AudioLanguageRepositoty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AudioLanguageService(IAudioLanguageRepository AudioLanguageRepositoty,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _AudioLanguageRepositoty = AudioLanguageRepositoty;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<AudioLanguageCreateResponse>> Add(AudioLanguageCreateRequest AudioLanguageRequest, Guid CourseId)
        {
            try
            {
                var AudioLanguage = _mapper.Map<AudioLanguage>(AudioLanguageRequest);
                AudioLanguage.CourseId = CourseId;

                await _AudioLanguageRepositoty.CreateAsync(AudioLanguage);
                await _unitOfWork.SaveChangesAsync();

                return new Response<AudioLanguageCreateResponse>(
                    true,
                    _mapper.Map<AudioLanguageCreateResponse>(AudioLanguage)
                );
            }
            catch (Exception ex)
            {
                return new Response<AudioLanguageCreateResponse>(false, ex.Message, null);
            }
        }
    }
}
