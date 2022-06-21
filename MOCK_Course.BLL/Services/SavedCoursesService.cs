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

            if (await _savedCoursesRepository.CheckExistSaveCourse(courseId, userId))
                return new DuplicateSaveCourseResponse(courseId, userId);

            var savecourse = new SavedCourses()
            {
                UserId = userId,
                CourseId = courseId,
            };

            await _savedCoursesRepository.CreateAsync(savecourse);
            await _unitOfWork.SaveChangesAsync();

            var savecourseDTO = _mapper.Map<SavedCoursesDTO>(savecourse);
            return new ApiOkResponse<SavedCoursesDTO>(savecourseDTO);

        }

        public async Task<ApiBaseResponse> GetAll(Guid userId, SavedCoursesParameters parameters)
        {
            var courses = await _savedCoursesRepository.GetAllSavedCourses(userId, parameters);

            return new ApiOkResponse<PagedList<SavedCoursesDTO>>(courses);
        }

        public async Task<ApiOkResponse<bool>> IsSaveCourses(Guid userId, Guid courseId)
        {

            var savedCourses = await _savedCoursesRepository.BuildQuery()
                                                            .FilterByUserId(userId)
                                                            .FilterByCourseId(courseId)
                                                            .AnyAsync();

            return new ApiOkResponse<bool>(savedCourses);
        }


        public async Task<ApiBaseResponse> Remove(Guid courseId, Guid userId)
        {
            var savecourseEntity = await _savedCoursesRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .FilterByCourseId(courseId)
                                                                .AsSelectorAsync(c => c);

            if (savecourseEntity is null)
                return new CourseNotFoundResponse(courseId);

            _savedCoursesRepository.Remove(savecourseEntity, permanent: true);
            await _unitOfWork.SaveChangesAsync();

            return new ApiBaseResponse(true);
        }
        public async Task<ApiBaseResponse> RemoveAll(Guid userId)
        {
            var savedCourses = await _savedCoursesRepository.BuildQuery()
                                                            .FilterByUserId(userId)
                                                            .ToListAsync(c => _mapper.Map<SavedCourses>(c));
            _savedCoursesRepository.RemoveRange(savedCourses);
            await _unitOfWork.SaveChangesAsync();
            return new ApiBaseResponse(true);
        }
    }
}
