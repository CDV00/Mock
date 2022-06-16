﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.Services.Abstraction;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;

namespace Course.BLL.Services
{
    public class SavedCoursesService : ISavedCoursesService
    {
        private readonly ISavedCoursesRepository _savedCoursesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SavedCoursesService(ISavedCoursesRepository savedCoursesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _savedCoursesRepository = savedCoursesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<SavedCoursesDTO>> Add(Guid userId, Guid courseId)
        {
            try
            {

                var savecourse = new SavedCourses()
                {
                    UserId = userId,
                    CourseId = courseId,
                };

                var CheckExistSaveCourse = await _savedCoursesRepository.BuildQuery()
                                                                        .FilterByUserId(userId)
                                                                        .FilterByCourseId(courseId)
                                                                        .AnyAsync();

                if (CheckExistSaveCourse)
                {
                    return new Response<SavedCoursesDTO>(false, "Already saved this course", null);
                }

                var savecourseResponse = _mapper.Map<SavedCoursesDTO>(savecourse);
                await _savedCoursesRepository.CreateAsync(savecourse);
                await _unitOfWork.SaveChangesAsync();
                return new Response<SavedCoursesDTO>(true, savecourseResponse);
            }
            catch (Exception ex)
            {
                return new Response<SavedCoursesDTO>(false, ex.Message, null);
            }
        }

        public async Task<PagedList<SavedCoursesDTO>> GetAll(Guid userId, SavedCoursesParameters savedCoursesParameters)
        {

            var savedCourses = await _savedCoursesRepository.BuildQuery()
                                                            .FilterByUserId(userId)
                                                            .IncludeCourse()
                                                            .IncludeUser()
                                                            .IncludeCategory()
                                                            .IncludeDiscount()
                                                            .Skip((savedCoursesParameters.PageNumber - 1) * savedCoursesParameters.PageSize)
                                                            .Take(savedCoursesParameters.PageSize)
                                                            .ToListAsync(c => _mapper.Map<SavedCoursesDTO>(c));

            var count = await _savedCoursesRepository.BuildQuery()
                                                     .FilterByUserId(userId)
                                                     .CountAsync();


            // TODO: use For to add total enroll and rating of course

            var pageList = new PagedList<SavedCoursesDTO>(savedCourses, count, savedCoursesParameters.PageNumber, savedCoursesParameters.PageSize);

            return pageList;

        }
        public async Task<BaseResponse> IsSaveCourses(Guid userId, Guid courseId)
        {
            try
            {
                var savedCourses = await _savedCoursesRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .FilterByCourseId(courseId)
                                                                .AsSelectorAsync(c => _mapper.Map<SavedCoursesDTO>(c));

                if (savedCourses == null)
                    return new BaseResponse(false);

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }


        public async Task<BaseResponse> Remove(Guid courseId, Guid userId)
        {
            try
            {
                var course = await _savedCoursesRepository.BuildQuery()
                                                          .FilterByUserId(userId)
                                                          .FilterByCourseId(courseId)
                                                          .AsSelectorAsync(c => c);
                if (course is null)
                {
                    return new BaseResponse(false, null, "Can't find course");
                }

                _savedCoursesRepository.Remove(course, true);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse { IsSuccess = true };

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }
        public async Task<BaseResponse> RemoveAll(Guid userId)
        {
            try
            {
                var savedCourses = await _savedCoursesRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .ToListAsync(c => _mapper.Map<SavedCourses>(c));
                _savedCoursesRepository.RemoveRange(savedCourses);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }
        //
        public async Task<bool> IsSavedCourse(Guid userId, Guid courseId)
        {
            bool savedCourse = await _savedCoursesRepository.BuildQuery()
                                                            .FilterByUserId(userId)
                                                            .FilterByCourseId(courseId)
                                                            .AnyAsync();

            return savedCourse;
        }
    }
}
