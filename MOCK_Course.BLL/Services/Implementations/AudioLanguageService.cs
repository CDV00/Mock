using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
        public async Task<Response<AudioLanguageDTO>> Add(AudioLanguageForCreateRequest AudioLanguageRequest, Guid courseId)
        {
            try
            {
                var AudioLanguage = _mapper.Map<AudioLanguage>(AudioLanguageRequest);
                AudioLanguage.CourseId = courseId;

                await _AudioLanguageRepositoty.CreateAsync(AudioLanguage);
                await _unitOfWork.SaveChangesAsync();

                return new Response<AudioLanguageDTO>(
                    true,
                    _mapper.Map<AudioLanguageDTO>(AudioLanguage)
                );
            }
            catch (Exception ex)
            {
                return new Response<AudioLanguageDTO>(false, ex.Message, null);
            }
        }

    }
}
