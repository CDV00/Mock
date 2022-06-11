using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.Responses;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Course.BLL.Share.RequestFeatures;

namespace Course.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICousesRepository _cousesRepository;
        private readonly IAudioLanguageRepository _audioLanguageRepository;
        private readonly ICloseCaptionRepository _closeCaptionRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly ICourseReviewService _courseReviewService;
        private readonly IEnrollmentService _enrollmentService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(ICousesRepository cousesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork, IAudioLanguageRepository audioLanguageRepository,
            ICloseCaptionRepository closeCaptionRepository, ILevelRepository levelRepository, ICourseReviewService courseReviewService, IEnrollmentService enrollmentService)
        {
            _cousesRepository = cousesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _audioLanguageRepository = audioLanguageRepository;
            _closeCaptionRepository = closeCaptionRepository;
            _levelRepository = levelRepository;
            _courseReviewService = courseReviewService;
            _enrollmentService = enrollmentService;
        }

        public async Task<PagedList<CourseDTO>> GetCoursesAsync(CourseParameters courseParameter)
        {
            var courses = await _cousesRepository.BuildQuery()
                                                 .IncludeCategory()
                                                 .IncludeUser()
                                                 .IncludeDiscount()
                                                 .FilterByKeyword(courseParameter.Keyword)
                                                 .FilterByUserId(courseParameter.userId)
                                                 .FilterByCategoryId(courseParameter.CategoryId)
                                                 .FilterByAudioLanguageIds(courseParameter.AudioLanguageIds)
                                                 .FilterByCloseCaptionIds(courseParameter.CloseCaptionIds)
                                                 .FilterByLevelIds(courseParameter.LevelIds)
                                                 .FilterByDiscount(courseParameter.IsDiscount)
                                                 .FilterByPrice(courseParameter.IsFree, courseParameter.IsDiscount)
                                                 .ApplySort(courseParameter.Orderby)
                                                 .Skip((courseParameter.PageNumber - 1) * courseParameter.PageSize)
                                                 .Take(courseParameter.PageSize)
                                                 .ToListAsync(c => _mapper.Map<CourseDTO>(c));

            var count = await _cousesRepository.BuildQuery()
                                               .FilterByKeyword(courseParameter.Keyword)
                                               .FilterByUserId(courseParameter.userId)
                                               .FilterByCategoryId(courseParameter.CategoryId)
                                               .FilterByAudioLanguageIds(courseParameter.AudioLanguageIds)
                                               .FilterByCloseCaptionIds(courseParameter.CloseCaptionIds)
                                               .FilterByLevelIds(courseParameter.LevelIds)
                                               .FilterByDiscount(courseParameter.IsDiscount)
                                               .FilterByPrice(courseParameter.IsFree, courseParameter.IsDiscount)
                                               .FilterByRating(courseParameter.Rate)
                                               .CountAsync();

            await AddRating(courses);

            var pageList = new PagedList<CourseDTO>(courses, count, courseParameter.PageNumber, courseParameter.PageSize);

            return pageList;
        }

        private async Task AddRating(List<CourseDTO> courses)
        {
            //for (var i = 0; i < courses.Count; i++)
            //{
            //courses[i].TotalEnroll = (await _enrollmentService.GetTotalEnrollOfCourse(courses[i].Id)).data;
            //}
        }

        /// <summary>
        /// làm thêm phân trang và filter ở đâu
        /// </summary>
        /// <returns></returns>
        public async Task<Response<CourseDTO>> GetAll()
        {
            try
            {
                var courses = await _cousesRepository.BuildQuery()
                                                     .IncludeCategory()
                                                     .IncludeUser()
                                                     .IncludeEnrolment()
                                                     .AsSelectorAsync(x => _mapper.Map<CourseDTO>(x));

                return new Response<CourseDTO>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<CourseDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<CourseDTO>> GetAllMyCoures(Guid userId)
        {
            try
            {
                var courses = await _cousesRepository.BuildQuery()
                                                     .IncludeCategory()
                                                     .IncludeUser()
                                                     .IncludeDiscount()
                                                     .FilterByUserId(userId)
                                                     .ToListAsync(c => _mapper.Map<CourseDTO>(c));

                await AddRating(courses);
                return new Responses<CourseDTO>(true, courses);
            }
            catch (Exception ex)
            {
                return new Responses<CourseDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<CourseDTO>> GetAllMyPurchase(Guid userId)
        {
            try
            {
                var courses = await _cousesRepository.BuildQuery()
                                                     .IncludeCategory()
                                                     .IncludeUser()
                                                     .IncludeDiscount()
                                                     .FilterByOrderd(userId)
                                                     .ToListAsync(c => _mapper.Map<CourseDTO>(c));

                await AddRating(courses);
                return new Responses<CourseDTO>(true, courses);
            }
            catch (Exception ex)
            {
                return new Responses<CourseDTO>(false, ex.Message, null);
            }
        }


        public async Task<Response<CourseDTO>> Get(Guid id)
        {
            try
            {
                var course = await _cousesRepository.BuildQuery()
                                                    .IncludeCategory()
                                                    .IncludeLanguage()
                                                    .IncludeLevel()
                                                    .IncludeSection()
                                                    .IncludeUser()
                                                    .FilterById(id)
                                                    .AsSelectorAsync(x => _mapper.Map<CourseDTO>(x));
                await AddRate(course);
                var courseResponse = _mapper.Map<CourseDTO>(course);
                return new Response<CourseDTO>(true, courseResponse);
            }
            catch (Exception ex)
            {
                return new Response<CourseDTO>(false, ex.Message, null);
            }
        }

        private async Task AddRate(CourseDTO course)
        {
            //course.Rating = (await _courseReviewService.GetAVGRatinng(course.Id, null)).data;
            course.TotalEnroll = (await _enrollmentService.GetTotalEnrollOfCourse(course.Id)).data;
        }

        public async Task<Response<CourseDTO>> Add(Guid userId, CourseForCreateRequest courseRequest)
        {
            try
            {
                var course = _mapper.Map<Courses>(courseRequest);
                course.UserId = userId;
                course.CreatedAt = DateTime.Now;

                await _cousesRepository.CreateAsync(course);

                #region add sub table
                List<AudioLanguage> audioLanguages = new();
                if (courseRequest.AudioLanguageIds != null)
                {
                    foreach (var id in courseRequest.AudioLanguageIds)
                    {
                        var audioLanguage = await _audioLanguageRepository.GetByIdAsync(id);
                        audioLanguages.Add(audioLanguage);
                    }
                    course.AudioLanguages = audioLanguages;
                }

                //course.AudioLanguages.Clear();

                List<CloseCaption> closeCaptions = new();
                if (courseRequest.CloseCaptionIds != null)
                {
                    foreach (var id in courseRequest.CloseCaptionIds)
                    {
                        var closeCaption = await _closeCaptionRepository.GetByIdAsync(id);
                        closeCaptions.Add(closeCaption);
                    }
                    //course.CloseCaptions.Clear();
                    course.CloseCaptions = closeCaptions;
                }

                List<Level> levels = new();
                if (courseRequest.LevelIds != null)
                {
                    foreach (var id in courseRequest.LevelIds)
                    {
                        var level = await _levelRepository.GetByIdAsync(id);
                        levels.Add(level);
                    }
                    //course.Levels.Clear();
                    course.Levels = levels;
                }
                #endregion

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


        public async Task<BaseResponse> Remove(Guid idCourse, Guid userId)
        {
            try
            {
                var course = await _cousesRepository.GetByIdAsync(idCourse);
                if (course == null)
                {
                    new Responses<BaseResponse>(false, "can't find course", null);
                }

                if (course.UserId != userId)
                    return new Response<CourseDTO>(false, "You are't the owner of the course", null);

                _cousesRepository.Remove(course, false);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse(true);

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<CourseDTO>> Update(Guid id, CourseForUpdateRequest courseRequest, Guid userId)
        {
            try
            {
                var course = await _cousesRepository.BuildQuery()
                                                    .FilterById(id)
                                                    .IncludeCategory()
                                                    .IncludeLevel()
                                                    .IncludeLanguage()
                                                    //.IncludeSection()
                                                    .AsSelectorAsync(c => c);


                if (course == null)
                {
                    return new Response<CourseDTO>(false, "can't find course", null);
                }
                if (course.UserId != userId)
                    return new Response<CourseDTO>(false, "You are't the owner of the course", null);

                AddNewSection(courseRequest);
                _mapper.Map(courseRequest, course);

                course.UpdatedAt = DateTime.Now;
                course.UpdatedBy = id;

                #region
                List<AudioLanguage> audioLanguages = new();
                if (courseRequest.AudioLanguageIds != null)
                {
                    foreach (var idcourse in courseRequest.AudioLanguageIds)
                    {
                        var audioLanguage = await _audioLanguageRepository.GetByIdAsync(idcourse);
                        audioLanguages.Add(audioLanguage);
                    }
                    //course.AudioLanguages.Clear();

                    course.AudioLanguages = audioLanguages;
                }

                //course.AudioLanguages.Clear();

                List<CloseCaption> closeCaptions = new();
                if (courseRequest.CloseCaptionIds != null)
                {
                    foreach (var idcourse in courseRequest.CloseCaptionIds)
                    {
                        var closeCaption = await _closeCaptionRepository.GetByIdAsync(idcourse);
                        closeCaptions.Add(closeCaption);
                    }
                    //course.CloseCaptions.Clear();
                    course.CloseCaptions = closeCaptions;
                }

                List<Level> levels = new();
                if (courseRequest.LevelIds != null)
                {
                    foreach (var idcourse in courseRequest.LevelIds)
                    {
                        var level = await _levelRepository.GetByIdAsync(idcourse);
                        levels.Add(level);
                    }
                    //course.Levels.Clear();
                    course.Levels = levels;
                }
                #endregion

                _cousesRepository.Update(course);
                var CourseResponse = _mapper.Map<CourseDTO>(course);

                await _unitOfWork.SaveChangesAsync();


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

        private static void AddNewSection(CourseForUpdateRequest courseRequest)
        {
            var sections = courseRequest.Sections;
            if (sections == null)
                return;

            for (var i = 0; i < sections.Count; i++)
            {
                var section = sections[i];

                var lectures = section.Lectures;
                if (lectures != null)
                {
                    for (var j = 0; j < lectures.Count; j++)
                    {
                        var lecture = lectures[j];

                        if (!lecture.IsNew)
                            continue;

                        lecture.Id = Guid.Empty;
                    }
                }

                if (!section.IsNew)
                    continue;

                section.Id = Guid.Empty;
            }
        }

        public async Task<Response<int>> GetTotalCourseOfUser(Guid userId)
        {
            try
            {
                var courses = await _cousesRepository.BuildQuery()
                                                     .FilterByUserId(userId)
                                                     .CountAsync();

                return new Response<int>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }
    }
}
