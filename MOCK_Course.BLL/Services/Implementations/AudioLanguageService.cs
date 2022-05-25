using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Responsesnamespace;
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

        public async Task<BaseResponse> RemoveAll(Guid courseId)
        {
            try
            {
                var audioLanguages = await _AudioLanguageRepositoty.GetAll().Where(a => a.CourseId == courseId).ToListAsync();

                foreach(var item in audioLanguages)
                {
                    _AudioLanguageRepositoty.Remove(item);
                }

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }
    }
}
