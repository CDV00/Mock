using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Course.BLL.Responses;
using Course.DAL.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Course.BLL.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICousesRepository _cousesRepository;
        private readonly IAudioLanguageRepository _audioLanguageRepository;
        private readonly ICloseCaptionRepository _closeCaptionRepository;
        private readonly ICourseLevelRepository _courseLevelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(ICousesRepository cousesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork, IAudioLanguageRepository audioLanguageRepository, ICloseCaptionRepository closeCaptionRepository, ICourseLevelRepository courseLevelRepository)
        {
            _cousesRepository = cousesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _audioLanguageRepository = audioLanguageRepository;
            _closeCaptionRepository = closeCaptionRepository;
            _courseLevelRepository = courseLevelRepository;
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
                course.UserId = userId;

                await _cousesRepository.CreateAsync(course);

                var result = await _unitOfWork.SaveChangesAsync();

                var CourseDTO = _mapper.Map<CourseDTO>(course);
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

                // remove language
                await _audioLanguageRepository.RemoveAll(Id);
                await _closeCaptionRepository.RemoveAll(Id);
                await _courseLevelRepository.RemoveAll(Id);

                _mapper.Map(courseRequest, course);

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
        public async Task<Response<int>> GetTotal(Guid userId)
        {
            try
            {
                var courses = await _cousesRepository.GetTotal(userId);
                return new Response<int>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }
        public async Task<Responses<MyCoursesDTO>> GetAllMyCoures(Guid userId)
        {
            try
            {
                var myCourse = await _cousesRepository.GetAllMyCoures(userId);
                return new Responses<MyCoursesDTO>(true, myCourse);
            }
            catch (Exception ex)
            {
                return new Responses<MyCoursesDTO>(false, ex.Message, null);
            }
        }
        public async Task<Responses<UpcommingCourseDTO>> GetAllUpcomingCourses(Guid userId)
        {
            try
            {
                var myCourse = await _cousesRepository.GetAllUpcomingCourses(userId);
                return new Responses<UpcommingCourseDTO>(true, myCourse);
            }
            catch (Exception ex)
            {
                return new Responses<UpcommingCourseDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<PurchaseDTO>> GetAllMyPurchase(Guid userId)
        {
            try
            {
                var myPurchase = await _cousesRepository.GetAllMyPurchase(userId);
                return new Responses<PurchaseDTO>(true, myPurchase);
            }
            catch (Exception ex)
            {
                return new Responses<PurchaseDTO>(false, ex.Message, null);
            }
        }
    }
}
