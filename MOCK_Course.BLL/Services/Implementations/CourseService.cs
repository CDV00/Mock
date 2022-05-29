using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Course.BLL.Responses;
using Course.DAL.Repositories.Implementations;

namespace Course.BLL.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICousesRepository _cousesRepository;
        private readonly IAudioLanguageRepository _audioLanguageRepository;
        private readonly ICloseCaptionRepository _closeCaptionRepository;
        private readonly ISectionRepositoty _sectionRepositoty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(ICousesRepository cousesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork, IAudioLanguageRepository audioLanguageRepository, ICloseCaptionRepository closeCaptionRepository, ISectionRepositoty sectionRepositoty)
        {
            _cousesRepository = cousesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _audioLanguageRepository = audioLanguageRepository;
            _closeCaptionRepository = closeCaptionRepository;
            _sectionRepositoty = sectionRepositoty;
        }

        /// <summary>
        /// làm thêm phân trang và filter ở đây
        /// </summary>
        /// <returns></returns>
        public async Task<Responses<CoursesCardDTO>> GetAll()
        {
            try
            {
                var courses = await _cousesRepository.GetAllForCard();

                var courseResponse = _mapper.Map<List<CoursesCardDTO>>(courses);
                return new Responses<CoursesCardDTO>(true, courseResponse);
            }
            catch (Exception ex)
            {
                return new Responses<CoursesCardDTO>(false, ex.Message, null);
            }
        }

        public async Task<Response<CourseDTO>> GetForPost(Guid id)
        {
            try
            {
                var course = await _cousesRepository.GetForPost(id);

                var courseResponse = _mapper.Map<CourseDTO>(course);
                return new Response<CourseDTO>(true, courseResponse);
            }
            catch (Exception ex)
            {
                return new Response<CourseDTO>(false, ex.Message, null);
            }
        }

        public async Task<Response<CourseDTO>> Add(Guid userId, CourseForCreateRequest courseRequest)
        {
            try
            {
                var course = _mapper.Map<Courses>(courseRequest);
                var courseId = Guid.NewGuid();
                course.UserId = userId;
                course.Id = courseId;

                await _cousesRepository.CreateAsync(course);

                var CourseDTO = _mapper.Map<CourseDTO>(course);

                // add language 
                await AddLanguages(courseRequest, courseId);

                await _unitOfWork.SaveChangesAsync();

                return new Response<CourseDTO>(
                    true,
                    CourseDTO
                );
            }
            catch (Exception ex)
            {
                return new Response<CourseDTO>(false, ex.Message, null);
            }
        }

        private async Task AddLanguages(CourseForCreateRequest courseRequest, Guid courseId)
        {
            var AudioLanguageLength = courseRequest.AudioLanguages.Count;
            var CloseCaptionLength = courseRequest.CloseCaptions.Count;

            if (AudioLanguageLength > 0)
            {
                for (var i = 0; i < AudioLanguageLength; i++)
                {
                    var audioLanguage = _mapper.Map<AudioLanguage>(courseRequest.AudioLanguages[i]);
                    audioLanguage.CourseId = courseId;
                    await _audioLanguageRepository.CreateAsync(audioLanguage);
                }
            }

            if (CloseCaptionLength > 0)
            {
                for (var i = 0; i < CloseCaptionLength; i++)
                {
                    var closeCaption = _mapper.Map<CloseCaption>(courseRequest.CloseCaptions[i]);
                    closeCaption.CourseId = courseId;
                    await _closeCaptionRepository.CreateAsync(closeCaption);
                }
            }
        }

        public async Task<BaseResponse> Remove(Guid idCourse)
        {
            try
            {
                var course = await _cousesRepository.GetByIdAsync(idCourse);
                if (course == null)
                {
                    new Responses<BaseResponse>(false, "can't find course", null);
                }

                _cousesRepository.Remove(course);
                await _sectionRepositoty.RemoveByCourseId(idCourse);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse(true);

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }


        public async Task<Response<CourseDTO>> Update(Guid Id, CourseForUpdateRequest courseRequest)
        {
            try
            {
                var course = await _cousesRepository.GetByIdAsync(Id);
                if (course == null)
                {
                    new Responses<BaseResponse>(false, "can't find course", null);
                }

                _mapper.Map(courseRequest, course);

                // remove language
                await _audioLanguageRepository.RemoveAll(Id);
                await _closeCaptionRepository.RemoveAll(Id);

                await AddLanguages(courseRequest, Id);

                await _unitOfWork.SaveChangesAsync();

                var CourseResponse = _mapper.Map<CourseDTO>(course);

                return new Response<CourseDTO>(
                    true,
                    CourseResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<CourseDTO>(false, ex.Message, null);
            }
        }
    }
}
