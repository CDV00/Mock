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
        public async Task<Responses<CoursesCardResponse>> GetAll()
        {
            try
            {
                var courses = await _cousesRepository.GetAll().Include(c => c.Category).Include(c => c.User).ToListAsync();

                var courseResponse = _mapper.Map<List<CoursesCardResponse>>(courses);
                return new Responses<CoursesCardResponse>(true, courseResponse);
            }
            catch (Exception ex)
            {
                return new Responses<CoursesCardResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<CourseResponse>> Get(Guid id)
        {
            try
            {
                var courses = await _cousesRepository.FindByCondition(c => c.Id == id).Include(c => c.AudioLanguages).Include(c => c.CloseCaptions).FirstOrDefaultAsync();

                var courseResponse = _mapper.Map<CourseResponse>(courses);
                return new Response<CourseResponse>(true, courseResponse);
            }
            catch (Exception ex)
            {
                return new Response<CourseResponse>(false, ex.Message, null);
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
                        await _closeCaptionService.Add(courseRequest.CloseCaptions[i]);
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

        public async Task<BaseResponse> Remove(Guid idCourse)
        {
            try
            {
                Courses course = await CheckCourseExist(idCourse);

                _cousesRepository.Remove(course);
                _unitOfWork.SaveChanges();

                return new BaseResponse(true);

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

        private async Task<Courses> CheckCourseExist(Guid idCourse)
        {
            var course = await _cousesRepository.GetByIdAsync(idCourse);
            if (course == null)
            {
                new Responses<BaseResponse>(false, "can't find course", null);
            }

            return course;
        }

        public async Task<Response<CourseResponse>> Update(UpdateCourseRequest courseRequest)
        {
            try
            {
                var course = _mapper.Map<Courses>(courseRequest);

                var courseId = courseRequest.Id;
                // remove language
                await _audioLanguageService.RemoveAll(courseId);
                await _closeCaptionService.RemoveAll(courseId);

                // add language 
                var AudioLanguageLength = courseRequest.AudioLanguages?.Count;
                if (AudioLanguageLength > 0)
                {

                    for (var i = 0; i < AudioLanguageLength; i++)
                    {
                        await _audioLanguageService.Add(courseRequest.AudioLanguages[i], courseId);
                    }
                }

                var CloseCaptionLength = courseRequest.CloseCaptions?.Count;
                if (CloseCaptionLength > 0)
                {
                    for (var i = 0; i < CloseCaptionLength; i++)
                    {
                        await _closeCaptionService.Add(courseRequest.CloseCaptions[i]);
                    }
                }

                var result = _cousesRepository.Update(course);
                var CourseResponse = _mapper.Map<CourseResponse>(course);

                if (!result)
                {
                    return new Response<CourseResponse>(false, "can't find coures", null);
                }
                await _unitOfWork.SaveChangesAsync();

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
    }
}
