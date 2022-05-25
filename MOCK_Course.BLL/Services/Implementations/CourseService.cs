using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Course.BLL.Responses;

namespace Course.BLL.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICousesRepository _cousesRepository;
        private readonly IAudioLanguageService _audioLanguageService;
        private readonly ICloseCaptionService _closeCaptionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseService(ICousesRepository cousesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork, IAudioLanguageService audioLanguageService, ICloseCaptionService closeCaptionService)
        {
            _cousesRepository = cousesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _audioLanguageService = audioLanguageService;
            _closeCaptionService = closeCaptionService;
        }

        /// <summary>
        /// làm thêm phân trang và filter ở đây
        /// </summary>
        /// <returns></returns>
        public async Task<Responses<CoursesCartResponse>> GetAll()
        {
            try
            {
                var categories = await _cousesRepository.GetAll().ToListAsync();

                var courseResponse = _mapper.Map<List<CoursesCartResponse>>(categories);
                return new Responses<CoursesCartResponse>(true, courseResponse);
            }
            catch (Exception ex)
            {
                return new Responses<CoursesCartResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<CourseResponse>> Add(CourseRequest courseRequest)
        {
            try
            {
                var course = _mapper.Map<Courses>(courseRequest);

                await _cousesRepository.CreateAsync(course);
                await _unitOfWork.SaveChangesAsync();
                var CourseResponse = _mapper.Map<CourseResponse>(course);

                // add language 
                var AudioLanguageLength = courseRequest.AudioLanguages.Count;
                var courseId = course.Id;
                if (AudioLanguageLength > 0)
                {
                    for (var i = 0; i < AudioLanguageLength; i++)
                    {
                        await _audioLanguageService.Add(courseRequest.AudioLanguages[i], courseId);
                    }
                }

                var CloseCaptionLength = courseRequest.CloseCaptions.Count;
                if (CloseCaptionLength > 0)
                {
                    for (var i = 0; i < CloseCaptionLength; i++)
                    {
                        await _closeCaptionService.Add(courseRequest.CloseCaptions[i], courseId);
                    }
                }

                return new Response<CourseResponse>(
                    true,
                    CourseResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<CourseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Responsesnamespace.BaseResponse> Remove(Guid idCourse)
        {
            try
            {
                var result = await _cousesRepository.GetByIdAsync(idCourse);

                _cousesRepository.Remove(result);
                _unitOfWork.SaveChanges();

                return new BaseResponse(true);

            }
            catch (Exception ex)
            {
                return new Responses<Responsesnamespace.BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<CourseResponse>> Update(UpdateCourseRequest courseRequest)
        {
            try
            {
                var course = _mapper.Map<Courses>(courseRequest);

                _cousesRepository.Update(course);
                await _unitOfWork.SaveChangesAsync();

                return new Response<CourseResponse>(
                    true,
                    _mapper.Map<CourseResponse>(course)
                );
            }
            catch (Exception ex)
            {
                return new Response<CourseResponse>(false, ex.Message, null);
            }
        }
    }
}
