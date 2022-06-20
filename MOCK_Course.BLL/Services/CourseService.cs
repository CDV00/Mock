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
using Entities.Responses;
using Entities.ParameterRequest;

namespace Course.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _cousesRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAudioLanguageRepository _audioLanguageRepository;
        private readonly ICloseCaptionRepository _closeCaptionRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly ISectionRepositoty _sectionRepositoty;
        private readonly ICourseReviewService _courseReviewService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly ISavedCoursesService _savedCoursesService;
        private readonly ISectionService _sectionService;

        private readonly ILectureService _lectureService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository cousesRepository,
                             IMapper mapper,
                             IUnitOfWork unitOfWork,
                             IAudioLanguageRepository audioLanguageRepository,
                             ICloseCaptionRepository closeCaptionRepository,
                             ILevelRepository levelRepository,
                             ISectionRepositoty sectionRepositoty,
                             ICourseReviewService courseReviewService,
                             IEnrollmentService enrollmentService,
                             ISavedCoursesService savedCoursesService,
                             ISectionService sectionService,
                             ILectureService lectureService,
                             ICategoryRepository categoryRepository)
        {
            _cousesRepository = cousesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _audioLanguageRepository = audioLanguageRepository;
            _closeCaptionRepository = closeCaptionRepository;
            _levelRepository = levelRepository;
            _sectionRepositoty = sectionRepositoty;
            _courseReviewService = courseReviewService;
            _enrollmentService = enrollmentService;
            _savedCoursesService = savedCoursesService;
            _sectionService = sectionService;
            _lectureService = lectureService;
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiBaseResponse> GetAllCourses(CourseParameters parameter, Guid? userId)
        {
            var courses = await _cousesRepository.GetAllCourseAsync(parameter);
            await AddLast(courses, userId);

            return new ApiOkResponse<PagedList<CourseDTO>>(courses);
        }

        private async Task AddLast(List<CourseDTO> courses, Guid? userId)
        {
            if (userId == null)
                return;
            for (var i = 0; i < courses.Count; i++)
            {
                courses[i].IsSave = (await _savedCoursesService.IsSaveCourses(userId.GetValueOrDefault(), courses[i].Id)).data;

                courses[i].PercentCompletion = await _lectureService.PercentCourseCompletion(userId.GetValueOrDefault(), courses[i].Id);

                courses[i].IsEnroll = (await _enrollmentService.IsEnrollment(userId.GetValueOrDefault(), courses[i].Id)).data == null ? false : true;
            }
        }

        //public async Task<Responses<CourseDTO>> GetAllMyCoures(Guid userId)
        //{
        //    try
        //    {
        //        var courses = await _cousesRepository.BuildQuery()
        //                                             .FilterByUserId(userId)
        //                                             .FilterByApprove()
        //                                             .IncludeCategory()
        //                                             .IncludeSection()
        //                                             .IncludeOrder()
        //                                             .IncludeUser()
        //                                             .IncludeDiscount()
        //                                             .ToListAsync(c => _mapper.Map<CourseDTO>(c));

        //        return new Responses<CourseDTO>(true, courses);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<CourseDTO>(false, ex.Message, null);
        //    }
        //}
        public async Task<ApiBaseResponse> GetAllMyCoures(CourseParameters parameter, Guid? userId)
        {
            var courses = await _cousesRepository.GetAllMyCoures(userId, parameter);
            //await AddLast(courses, userId);

            return new ApiOkResponse<PagedList<CourseDTO>>(courses);
        }

        // Upcoming courses: Review
        //public async Task<Responses<CourseDTO>> UpcomingCourse(Guid userId)
        //{
        //    try
        //    {
        //        Status status = Status.Review;
        //        var courses = await _cousesRepository.BuildQuery()
        //                                             .FilterByUserId(userId)
        //                                             .FilterStatus(status)
        //                                             .IncludeCategory()
        //                                             .IncludeUser()
        //                                             .IncludeDiscount()
        //                                             .FilterByOrderd(userId)
        //                                             .ToListAsync(c => _mapper.Map<CourseDTO>(c));

        //        return new Responses<CourseDTO>(true, courses);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<CourseDTO>(false, ex.Message, null);
        //    }
        //}
        public async Task<ApiBaseResponse> UpcomingCourse(CourseParameters parameter, Guid? userId)
        {
            var courses = await _cousesRepository.UpcomingCourse(userId, parameter);

            return new ApiOkResponse<PagedList<CourseDTO>>(courses);
        }

        //public async Task<Responses<CourseDTO>> GetAllMyPurchase(Guid userId)
        //{
        //    try
        //    {
        //        var courses = await _cousesRepository.BuildQuery()
        //                                             .IncludeCategory()
        //                                             .IncludeUser()
        //                                             .IncludeDiscount()
        //                                             .FilterByOrderd(userId)
        //                                             .ToListAsync(c => _mapper.Map<CourseDTO>(c));

        //        return new Responses<CourseDTO>(true, courses);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<CourseDTO>(false, ex.Message, null);
        //    }
        //}

        public async Task<ApiBaseResponse> GetAllMyPurchase(CourseParameters parameter, Guid userId)
        {
            var courses = await _cousesRepository.GetAllMyPurchase(userId, parameter);

            return new ApiOkResponse<PagedList<CourseDTO>>(courses);
        }

        public async Task<ApiBaseResponse> GetDetail(Guid id, Guid? userId)
        {
            var course = await _cousesRepository.GetDetailCourseAsync(id);
            if (course is null)
                return new CourseNotFoundResponse(id);

            await AddLast(new List<CourseDTO> { course }, userId);

            return new ApiOkResponse<CourseDTO>(course);
        }

        public async Task<ApiBaseResponse> Add(Guid userId, CourseForCreateRequest courseRequest)
        {
            if (!await _categoryRepository.Existing(courseRequest.CategoryId))
                return new CategoryNotFoundResponse(courseRequest.CategoryId);

            if (!await _audioLanguageRepository.CheckExists(courseRequest.AudioLanguageIds))
                return new NotMathIdResponse(nameof(AudioLanguage), string.Join(',', courseRequest.AudioLanguageIds));

            if (!await _closeCaptionRepository.CheckExists(courseRequest.CloseCaptionIds))
                return new NotMathIdResponse(nameof(CloseCaption), string.Join(',', courseRequest.AudioLanguageIds));

            if (!await _levelRepository.CheckExists(courseRequest.LevelIds))
                return new NotMathIdResponse(nameof(Level), string.Join(',', courseRequest.AudioLanguageIds));

            var course = _mapper.Map<Courses>(courseRequest);
            course.UserId = userId;
            course.CreatedAt = DateTime.Now;

            await _cousesRepository.CreateAsync(course);

            GetTotalTimeOfCourse(course);

            var audioLanguageIds = courseRequest.AudioLanguageIds;
            var closeCaptionIds = courseRequest.CloseCaptionIds;
            var levelIds = courseRequest.LevelIds;
            await AddLanguages(audioLanguageIds, closeCaptionIds, levelIds, course);

            await _unitOfWork.SaveChangesAsync();

            var CourseDTO = _mapper.Map<CourseDTO>(course);
            return new ApiOkResponse<CourseDTO>(CourseDTO);
        }

        private static void GetTotalTimeOfCourse(Courses course)
        {
            if (course.Sections == null)
                return;
            var courseTime = 0;
            foreach (var section in course.Sections)
            {
                var sectionTime = 0;
                if (section.Lectures == null)
                    continue;

                foreach (var lecture in section.Lectures)
                {
                    if (lecture == null || lecture.IsDeleted)
                        continue;

                    sectionTime += lecture.TotalTime;
                    courseTime += lecture.TotalTime;
                }

                section.TotalTime = sectionTime;
            }
            course.TotalTime = courseTime;
        }


        public async Task<ApiBaseResponse> Remove(Guid idCourse, Guid userId)
        {
            var course = await _cousesRepository.GetByIdAsync(idCourse);
            if (course is null)
                return new CourseNotFoundResponse(idCourse);

            if (course.UserId != userId)
                return new NotOwnOfCourseResponse(idCourse);

            _cousesRepository.Remove(course, false);
            await _unitOfWork.SaveChangesAsync();

            return new ApiBaseResponse(true);
        }

        // Upload status course: Id course, status
        public async Task<BaseResponse> UpdateStatus(CourseStatusUpdateRequest courseStatusUpdateRequest)
        {
            try
            {

                var course = await _cousesRepository.BuildQuery()
                                                    .FilterById(courseStatusUpdateRequest.CourseId)
                                                    .AsSelectorAsync(c => c);

                if (course == null)
                {
                    return new Response<BaseResponse>(false, "can't find course", null);
                }
                /*if (course.UserId != userId)
                    return new Response<CourseDTO>(false, "You aren't the owner of the course", null);*/

                course.status = (Status)courseStatusUpdateRequest.status;
                await _unitOfWork.SaveChangesAsync();


                return new Response<CourseDTO>(
                    true
                );
            }
            catch (Exception ex)
            {
                return new Response<CourseDTO>(false, ex.Message, null);
            }
        }

        public async Task<ApiBaseResponse> Update(Guid id, CourseForUpdateRequest courseRequest, Guid userId)
        {
            var course = await _cousesRepository.BuildQuery()
                                                .FilterById(id)
                                                .IncludeCategory()
                                                .IncludeLevel()
                                                .IncludeLanguage()
                                                .IncludeSection()
                                                .IncludeAssignment()
                                                .IncludeQuiz()
                                                .AsSelectorAsync(c => c);

            if (course is null)
                return new CourseNotFoundResponse(id);

            if (course.UserId != userId)
                return new NotOwnOfCourseResponse(id);

            if (!await _audioLanguageRepository.CheckExists(courseRequest.AudioLanguageIds))
                return new NotMathIdResponse(nameof(AudioLanguage), string.Join(',', courseRequest.AudioLanguageIds));

            if (!await _categoryRepository.Existing(courseRequest.CategoryId))
                return new CategoryNotFoundResponse(courseRequest.CategoryId);

            if (!await _closeCaptionRepository.CheckExists(courseRequest.CloseCaptionIds))
                return new NotMathIdResponse(nameof(CloseCaption), string.Join(',', courseRequest.AudioLanguageIds));

            if (!await _levelRepository.CheckExists(courseRequest.LevelIds))
                return new NotMathIdResponse(nameof(Level), string.Join(',', courseRequest.AudioLanguageIds));

            AddNewSection(courseRequest);
            _mapper.Map(courseRequest, course);

            course.UpdatedAt = DateTime.Now;
            course.UpdatedBy = id;
            GetTotalTimeOfCourse(course);

            var audioLanguageIds = courseRequest.AudioLanguageIds;
            var closeCaptionIds = courseRequest.CloseCaptionIds;
            var levelIds = courseRequest.LevelIds;
            await AddLanguages(audioLanguageIds, closeCaptionIds, levelIds, course);

            _cousesRepository.Update(course);
            var CourseResponse = _mapper.Map<CourseDTO>(course);

            await _unitOfWork.SaveChangesAsync();

            return new ApiOkResponse<CourseDTO>(CourseResponse);
        }

        /// <summary>
        /// Clear and add audio language, close caption, level
        /// </summary>
        /// <param name="audioLanguageIds"></param>
        /// <param name="closeCaptionIds"></param>
        /// <param name="levelIds"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        private async Task AddLanguages(List<Guid> audioLanguageIds, List<Guid> closeCaptionIds, List<Guid> levelIds, Courses course)
        {
            List<AudioLanguage> audioLanguages = new();
            if (audioLanguageIds != null)
            {
                foreach (var id in audioLanguageIds)
                {
                    var audioLanguage = await _audioLanguageRepository.GetByIdAsync(id);
                    audioLanguages.Add(audioLanguage);
                }
                course.AudioLanguages = audioLanguages;
            }

            List<CloseCaption> closeCaptions = new();
            if (closeCaptionIds != null)
            {
                foreach (var id in closeCaptionIds)
                {
                    var closeCaption = await _closeCaptionRepository.GetByIdAsync(id);
                    closeCaptions.Add(closeCaption);
                }
                course.CloseCaptions = closeCaptions;
            }

            List<Level> levels = new();
            if (levelIds != null)
            {
                foreach (var idcourse in levelIds)
                {
                    var level = await _levelRepository.GetByIdAsync(idcourse);
                    levels.Add(level);
                }
                course.Levels = levels;
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

                if (section.IsNew)
                {
                    section.Id = Guid.Empty;
                }

                #region Update Leacture
                var lectures = section.Lectures;
                if (lectures != null)
                {
                    for (var j = 0; j < lectures.Count; j++)
                    {
                        var lecture = lectures[j];

                        if (lecture.IsNew)
                        {
                            lecture.Id = Guid.Empty;
                        }
                    }
                }
                #endregion
                #region Update Assignment
                var assignments = section.Assignments;
                if (assignments != null)
                {
                    for (var j = 0; j < assignments.Count; j++)
                    {
                        var assignment = assignments[j];

                        #region Update Attachment
                        var attachments = assignment.Attachments;
                        if (attachments != null)
                        {
                            for (var k = 0; k < attachments.Count; k++)
                            {
                                var attachment = attachments[k];

                                if (attachment.IsNew)
                                {
                                    attachment.Id = Guid.Empty;
                                }
                            }
                        }
                        #endregion

                        if (assignment.IsNew)
                        {
                            assignment.Id = Guid.Empty;
                        }
                    }
                }
                #endregion
                #region Update Quiz
                var quizzes = section.Quizzes;
                if (quizzes != null)
                {
                    for (var j = 0; j < quizzes.Count; j++)
                    {
                        var quiz = quizzes[j];

                        #region Update Question
                        var questions = quiz.Questions;
                        if (questions != null)
                        {
                            for (var k = 0; k < questions.Count; k++)
                            {
                                var question = questions[j];

                                #region Update QuizOptions
                                var quizoptions = question.Options;
                                if (quizoptions != null)
                                {
                                    for (var l = 0; l < quizoptions.Count; l++)
                                    {
                                        var quizoption = quizoptions[l];

                                        if (quizoption.IsNew)
                                        {
                                            quizoption.Id = Guid.Empty;
                                        }
                                    }
                                }
                                #endregion
                                if (question.IsNew)
                                {
                                    question.Id = Guid.Empty;
                                }
                            }
                        }
                        #endregion

                        #region Update QuizSetting
                        var settings = quiz.Settings;
                        if (settings != null)
                        {
                            for (var k = 0; k < settings.Count; j++)
                            {
                                var setting = settings[j];

                                if (setting.IsNew)
                                {
                                    setting.Id = Guid.Empty;
                                }
                            }
                        }
                        #endregion

                        if (quiz.IsNew)
                        {
                            quiz.Id = Guid.Empty;
                        }
                    }
                }
                #endregion
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


        /// <summary>
        /// làm thêm phân trang và filter ở đâu
        /// </summary>
        /// <returns></returns>
        //public async Task<Response<CourseDTO>> GetAll(Guid? userId)
        //{
        //    try
        //    {
        //        var courses = await _cousesRepository.BuildQuery()
        //                                             .IncludeCategory()
        //                                             .IncludeUser()
        //                                             .IncludeEnrolment()
        //                                             .AsSelectorAsync(x => _mapper.Map<CourseDTO>(x));
        //        if (userId != null)
        //        {
        //            //await AddLast(courses, userId);
        //        }
        //        return new Response<CourseDTO>(true, courses);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<CourseDTO>(false, ex.Message, null);
        //    }
        //}
    }
}
