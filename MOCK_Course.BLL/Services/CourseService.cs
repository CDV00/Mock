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
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        private readonly ICourseReviewRepository _courseReviewRepository;
        private readonly ICourseReviewService _courseReviewService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly ISavedCoursesService _savedCoursesService;
        private readonly ISectionService _sectionService;
        private readonly ILectureRepository _lectureRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizOptionRepository _quizOptionRepository;
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly ILectureService _lectureService;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAttachmentRepository _attachmentRepository;
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
                             IOrderService orderService,
                             ICategoryRepository categoryRepository,
                             IOrderItemService orderItemService,
                             IQuestionRepository questionRepository,
                             ICourseReviewRepository courseReviewRepository,
                             ILectureRepository lectureRepository,
                             IQuizRepository quizRepository,
                             IQuizOptionRepository quizOptionRepository,
                             IAttachmentRepository attachmentRepository,
                             IAssignmentRepository assignmentRepository
            )
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
            _orderService = orderService;
            _categoryRepository = categoryRepository;
            _orderItemService = orderItemService;
            _questionRepository = questionRepository;
            _courseReviewRepository = courseReviewRepository;
            _lectureRepository = lectureRepository;
            _quizRepository = quizRepository;
            _quizOptionRepository = quizOptionRepository;
            _attachmentRepository = attachmentRepository;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<ApiBaseResponse> GetAllCourses(CourseParameters parameter, Guid? userId)
        {
            var courses = await _cousesRepository.GetAllCourseAsync(parameter, userId);
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

                courses[i].IsPurchased = (await _orderItemService.IsPurchased(userId.GetValueOrDefault(), courses[i].Id)).data == null ? false : true;

                courses[i].MyRating = (await _courseReviewRepository.GetMyRating(userId.GetValueOrDefault(), courses[i].Id));
            }
        }

        public async Task<ApiBaseResponse> GetAllMyCoures(CourseParameters parameter, Guid userId)
        {
            var courses = await _cousesRepository.GetAllMyCoures(userId, parameter);

            return new ApiOkResponse<PagedList<CourseDTO>>(courses);
        }
        public async Task<ApiBaseResponse> GetAllMyLearning(CourseParameters parameter, Guid userId)
        {
            var courses = await _cousesRepository.GetAllMyLearning(userId, parameter);
            await AddLast(courses, userId);

            return new ApiOkResponse<PagedList<CourseDTO>>(courses);
        }
        public async Task<ApiBaseResponse> UpcomingCourse(CourseParameters parameter, Guid userId)
        {
            var courses = await _cousesRepository.UpcomingCourse(userId, parameter);

            return new ApiOkResponse<PagedList<CourseDTO>>(courses);
        }

        public async Task<ApiBaseResponse> GetAllMyPurchase(CourseParameters parameter, Guid userId)
        {
            var courses = await _cousesRepository.GetAllMyPurchase(userId, parameter);
            await AddLast(courses, userId);

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
                return new CategoryNotFoundResponse(courseRequest.CategoryId.GetValueOrDefault());

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
                                                .IncludeLevel()
                                                .IncludeLanguage()
                                                .AsSelectorAsync(c => c);

            if (course is null)
                return new CourseNotFoundResponse(id);

            if (course.UserId != userId)
                return new NotOwnOfCourseResponse(id);

            if (!await _audioLanguageRepository.CheckExists(courseRequest.AudioLanguageIds))
                return new NotMathIdResponse(nameof(AudioLanguage), string.Join(',', courseRequest.AudioLanguageIds));

            if (!await _categoryRepository.Existing(courseRequest.CategoryId))
                return new CategoryNotFoundResponse(courseRequest.CategoryId.GetValueOrDefault());

            if (!await _closeCaptionRepository.CheckExists(courseRequest.CloseCaptionIds))
                return new NotMathIdResponse(nameof(CloseCaption), string.Join(',', courseRequest.AudioLanguageIds));

            if (!await _levelRepository.CheckExists(courseRequest.LevelIds))
                return new NotMathIdResponse(nameof(Level), string.Join(',', courseRequest.AudioLanguageIds));

            await UpdateSection(courseRequest, id);

            _mapper.Map(courseRequest, course);

            course.UpdatedAt = DateTime.Now;
            course.UpdatedBy = id;
            GetTotalTimeOfCourse(course);

            var audioLanguageIds = courseRequest.AudioLanguageIds;
            var closeCaptionIds = courseRequest.CloseCaptionIds;
            var levelIds = courseRequest.LevelIds;
            await AddLanguages(audioLanguageIds, closeCaptionIds, levelIds, course);

            _cousesRepository.Update(course);

            await _unitOfWork.SaveChangesAsync();
            var CourseResponse = _mapper.Map<CourseDTO>(course);

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



        public async Task<Response<int>> GetTotalCourseOfUser(Guid userId)
        {
            try
            {
                var status = Status.Aprrove;
                var courses = await _cousesRepository.BuildQuery()
                                                     .FilterByUserId(userId)
                                                     .FilterStatus(status)
                                                     .CountAsync();

                return new Response<int>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }





















        private async Task UpdateSection(CourseForUpdateRequest courseRequest, Guid courseId)
        {
            if (courseRequest == null)
                return;

            var sections = _mapper.Map<List<Section>>(courseRequest.Sections.ToList());

            for (var i = 0; i < sections.Count; i++)
            {
                var lectures = _mapper.Map<List<Lecture>>(sections[i].Lectures.ToList());
                var quizs = _mapper.Map<List<Quiz>>(sections[i].Quizzes.ToList());
                var assignments = _mapper.Map<List<Assignment>>(sections[i].Assignments.ToList());
                sections[i].Lectures = null;
                sections[i].Quizzes = null;
                sections[i].Assignments = null;

                var section = sections[i];
                section.CourseId = courseId;
                if (!await _sectionRepositoty.FindByCondition(s => s.Id == section.Id)
                                             .AnyAsync())
                {
                    await _sectionRepositoty.CreateAsync(section);
                    await _unitOfWork.SaveChangesAsync();
                    _sectionRepositoty.ChangeDetachedState(section);
                }

                await UpdateLecture(lectures, section.Id);
                await UpdateQuiz(quizs, section.Id);
                await UpdateAssignment(assignments, section.Id);
            }
        }

        private async Task UpdateLecture(List<Lecture> lectures, Guid sectionId)
        {
            if (lectures == null || lectures.Count == 0)
                return;

            for (var i = 0; i < lectures.Count; i++)
            {

                Lecture lecture = lectures[i];
                if (!await _lectureRepository.FindByCondition(l => l.Id == lecture.Id)
                                             .AnyAsync())
                {
                    lecture.SectionId = sectionId;
                    await _lectureRepository.CreateAsync(lecture);
                    await _unitOfWork.SaveChangesAsync();
                    _lectureRepository.ChangeDetachedState(lecture);
                }
            }
        }

        private async Task UpdateQuiz(List<Quiz> quizs, Guid sectionId)
        {
            if (quizs == null || quizs.Count == 0)
                return;

            for (var i = 0; i < quizs.Count; i++)
            {
                Quiz quiz = quizs[i];
                var questions = quiz.Questions.ToList();
                quiz.Questions = null;

                if (!await _quizRepository.FindByCondition(l => l.Id == quiz.Id)
                                          .AnyAsync())
                {
                    quiz.SectionId = sectionId;
                    await _quizRepository.CreateAsync(quiz);
                    await _unitOfWork.SaveChangesAsync();
                    _quizRepository.ChangeDetachedState(quiz);
                }

                await UpdateQuestion(questions, quiz.Id);
            }
        }

        private async Task UpdateQuestion(List<Question> questions, Guid quizId)
        {
            if (questions == null || questions.Count == 0)
                return;

            for (var i = 0; i < questions.Count; i++)
            {
                Question question = questions[i];
                var options = question.Options.ToList();
                question.Options = null;

                if (!await _questionRepository.FindByCondition(l => l.Id == question.Id)
                                              .AnyAsync())
                {
                    question.QuizId = quizId;
                    await _questionRepository.CreateAsync(question);
                    await _unitOfWork.SaveChangesAsync();
                    _questionRepository.ChangeDetachedState(question);
                }

                await UpdateQuizOption(options, question.Id);
            }
        }

        private async Task UpdateQuizOption(List<QuizOption> quizOptions, Guid questionId)
        {
            if (quizOptions == null || quizOptions.Count == 0)
                return;
            for (var i = 0; i < quizOptions.Count; i++)
            {
                QuizOption quizOption = quizOptions[i];
                if (!await _quizOptionRepository.FindByCondition(l => l.Id == quizOption.Id)
                                                .AnyAsync())
                {
                    quizOption.QuestionId = questionId;
                    await _quizOptionRepository.CreateAsync(quizOption);
                    await _unitOfWork.SaveChangesAsync();
                    _quizOptionRepository.ChangeDetachedState(quizOption);
                }
            }
        }
        private async Task UpdateAssignment(List<Assignment> assigments, Guid sectionId)
        {
            if (assigments == null || assigments.Count == 0)
                return;

            for (var i = 0; i < assigments.Count; i++)
            {
                Assignment assignment = assigments[i];
                var attachments = assignment.Attachments.ToList();
                assignment.Attachments = null;

                if (!await _assignmentRepository.FindByCondition(l => l.Id == assignment.Id)
                                                .AnyAsync())
                {
                    assignment.SectionId = sectionId;
                    await _assignmentRepository.CreateAsync(assignment);
                    await _unitOfWork.SaveChangesAsync();
                    _assignmentRepository.ChangeDetachedState(assignment);
                }
                await UpdateAttachment(attachments, assignment.Id);
            }
        }

        private async Task UpdateAttachment(List<Attachment> attachments, Guid asignmentId)
        {
            if (attachments == null || attachments.Count == 0)
                return;

            for (var i = 0; i < attachments.Count; i++)
            {
                Attachment attachment = attachments[i];
                if (!await _attachmentRepository.FindByCondition(l => l.Id == attachment.Id)
                                                .AnyAsync())
                {
                    attachment.AssignmentId = asignmentId;
                    await _attachmentRepository.CreateAsync(attachment);
                    await _unitOfWork.SaveChangesAsync();
                    _attachmentRepository.ChangeDetachedState(attachment);
                }
            }
        }
    }
}

