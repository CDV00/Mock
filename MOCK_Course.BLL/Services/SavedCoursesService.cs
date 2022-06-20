using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Services.Abstraction;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using Entities.Responses;

namespace Course.BLL.Services
{
    public class SavedCoursesService : ISavedCoursesService
    {
        private readonly ISavedCoursesRepository _savedCoursesRepository;
        private readonly ICourseRepository _coursesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SavedCoursesService(ISavedCoursesRepository savedCoursesRepository, ICourseRepository coursesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _savedCoursesRepository = savedCoursesRepository;
            _coursesRepository = coursesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiBaseResponse> Add(Guid userId, Guid courseId)
        {
            if (!await _coursesRepository.IsExist(courseId))
                    return new CourseNotFoundResponse(courseId);

            var savecourse = new SavedCourses()
             {
                    UserId = userId,
                    CourseId = courseId,
            };

            if (await _savedCoursesRepository.CheckExistSaveCourse(courseId,userId))
                return new ExistSaveCourseResponse(courseId, userId);

            var savecourseEntity = _mapper.Map<SavedCourses>(savecourse);

            await _savedCoursesRepository.CreateAsync(savecourseEntity);
            await _unitOfWork.SaveChangesAsync();

            var savecourseDTO = _mapper.Map<SavedCoursesDTO>(savecourseEntity);
            return new ApiOkResponse<SavedCoursesDTO>(savecourseDTO);

        }

        //public async Task<PagedList<SavedCoursesDTO>> GetAll(Guid userId, SavedCoursesParameters savedCoursesParameters)
        //{

        //    var savedCourses = await _savedCoursesRepository.BuildQuery()
        //                                                    .FilterByUserId(userId)
        //                                                    .IncludeCourse()
        //                                                    .IncludeUser()
        //                                                    .IncludeCategory()
        //                                                    .IncludeDiscount()
        //                                                    .Skip((savedCoursesParameters.PageNumber - 1) * savedCoursesParameters.PageSize)
        //                                                    .Take(savedCoursesParameters.PageSize)
        //                                                    .ToListAsync(c => _mapper.Map<SavedCoursesDTO>(c));

        //    var count = await _savedCoursesRepository.BuildQuery()
        //                                             .FilterByUserId(userId)
        //                                             .CountAsync();


        //TODO: use For to add total enroll and rating of course

        //    var pageList = new PagedList<SavedCoursesDTO>(savedCourses, count, savedCoursesParameters.PageNumber, savedCoursesParameters.PageSize);

        //    return pageList;

        //}

        public async Task<ApiBaseResponse> GetAll(Guid userId, SavedCoursesParameters parameters)
        {
            var courses = await _savedCoursesRepository.GetAllSavedCourses(userId,parameters);

            return new ApiOkResponse<PagedList<SavedCoursesDTO>>(courses);
        }

        public async Task<Response<bool>> IsSaveCourses(Guid userId, Guid courseId)
        {
            try
            {
                var savedCourses = await _savedCoursesRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .FilterByCourseId(courseId)
                                                                .AnyAsync();

                return new Response<bool>(true, savedCourses);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, null);
            }
        }


        public async Task<ApiBaseResponse> Remove(Guid courseId, Guid userId)
        {
            //try
            //{
            //    var course = await _savedCoursesRepository.BuildQuery()
            //                                              .FilterByUserId(userId)
            //                                              .FilterByCourseId(courseId)
            //                                              .AsSelectorAsync(c => c);
            //    if (course is null)
            //    {
            //        return new BaseResponse(false, null, "Can't find course");
            //    }

            //    _savedCoursesRepository.Remove(course, true);
            //    await _unitOfWork.SaveChangesAsync();

            //    return new BaseResponse { IsSuccess = true };

            //}
            //catch (Exception ex)
            //{
            //    return new Responses<BaseResponse>(false, ex.Message, null);
            //}

            var savecourseEntity = await _savedCoursesRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .FilterByCourseId(courseId)
                                                                .AsSelectorAsync(c => c);

            if (!await _coursesRepository.IsExist(courseId))
                return new CourseNotFoundResponse(courseId);

            _savedCoursesRepository.Remove(savecourseEntity, permanent: true);
            await _unitOfWork.SaveChangesAsync();

            return new ApiBaseResponse(true);
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
