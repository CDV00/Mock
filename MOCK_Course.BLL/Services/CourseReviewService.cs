using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Course.BLL.Share.RequestFeatures;
using Entities.ParameterRequest;
using Entities.Responses;

namespace Course.BLL.Services
{
    public class CourseReviewService : ICourseReviewService
    {
        private readonly ICourseRepository _cousesRepository;
        private readonly ICourseReviewRepository _courseReviewRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseReviewService(ICourseReviewRepository courseReviewRepository, ICourseRepository cousesRepository, IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _courseReviewRepository = courseReviewRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _cousesRepository = cousesRepository;
        }


        // Todo: filter and paging
        /// <summary>
        /// Get all course review
        /// </summary>
        /// <returns></returns>
        public async Task<ApiBaseResponse> GetAll(Guid courseId, CourseReviewParameters parameter)
        {
            var coursereview = await _courseReviewRepository.GetAllCourseReview(courseId, parameter);

            return new ApiOkResponse<PagedList<CourseReviewDTO>>(coursereview);
        }

        public async Task<ApiBaseResponse> Add(CourseReviewRequest courseReviewRequest)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(courseReviewRequest.EnrollmentId);
            if (enrollment is null)
                return new EnrollmentNotFoundResponse(courseReviewRequest.EnrollmentId);

            var courseReview = _mapper.Map<CourseReview>(courseReviewRequest);
            courseReview.CreatedAt = DateTime.Now;
            await _courseReviewRepository.CreateAsync(courseReview);

            await IncreateRate(enrollment, courseReview);

            await _unitOfWork.SaveChangesAsync();
            var courseReviewDTO = _mapper.Map<CourseReviewDTO>(courseReview);
            return new ApiOkResponse<CourseReviewDTO>(courseReviewDTO);
        }

        private async Task IncreateRate(Enrollment enrollment, CourseReview courseReview)
        {
            var course = await _cousesRepository.GetByIdAsync(enrollment.CourseId);
            course.SumRates += courseReview.Rating;
            course.TotalReviews++;
            if (!ValidAvgRate(course))
            {
                course.AvgRate = 0;
                return;
            }
            course.AvgRate = course.SumRates / course.TotalReviews;
        }

        /// <summary>
        /// update course review
        /// </summary>
        /// <param name="courseReviewUpdateRequest"></param>
        /// <returns></returns>
        public async Task<ApiBaseResponse> Update(Guid id, CourseReviewUpdateRequest courseReviewUpdateRequest, Guid userId)
        {


            var courseReview = await _courseReviewRepository.BuildQuery()
                                                            .GetById(id)
                                                            .IncludeEnrollment()
                                                            .AsSelectorAsync(c => c);

            if (courseReview == null)
                return new CourseReviewNotFound(id);

            var enrollment = await _enrollmentRepository.GetByIdAsync(courseReview.EnrollmentId);
            if (enrollment.UserId != userId)
                return new NotOwnOfCourseReviewResponse(courseReview.Id);

            courseReview.UpdatedAt = DateTime.Now;

            await UpdateRate(courseReviewUpdateRequest, courseReview);

            _mapper.Map(courseReviewUpdateRequest, courseReview);
            await _unitOfWork.SaveChangesAsync();
            var courseReviewDTO = _mapper.Map<CourseReviewDTO>(courseReview);
            return new ApiOkResponse<CourseReviewDTO>(courseReviewDTO);
        }

        private async Task UpdateRate(CourseReviewUpdateRequest courseReviewUpdateRequest, CourseReview courseReview)
        {
            var course = await _cousesRepository.GetByIdAsync(courseReview.Enrollment.CourseId);
            course.SumRates -= courseReview.Rating;
            course.SumRates += courseReviewUpdateRequest.Rating;
            if (!ValidAvgRate(course))
            {
                course.AvgRate = 0;
                return;
            }
            course.AvgRate = course.SumRates / course.TotalReviews;
        }

        /// <summary>
        /// delete course review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiBaseResponse> Delete(Guid id, Guid userId)
        {
            var courseReview = await _courseReviewRepository.BuildQuery()
                                                            .GetById(id)
                                                            .IncludeEnrollment()
                                                            .AsSelectorAsync(c => c);
            if (courseReview == null)
                return new CourseReviewNotFound(id);

            var enrollment = await _enrollmentRepository.GetByIdAsync(courseReview.EnrollmentId);
            if (enrollment.UserId != userId)
                return new NotOwnOfCourseReviewResponse(courseReview.Id);

            await RemoveRateCourse(courseReview);

            _courseReviewRepository.Remove(courseReview, false);
            await _unitOfWork.SaveChangesAsync();
            return new ApiBaseResponse(true);
        }

        private async Task RemoveRateCourse(CourseReview courseReview)
        {
            var course = await _cousesRepository.GetByIdAsync(courseReview.Enrollment.CourseId);
            course.SumRates -= courseReview.Rating;
            course.TotalReviews--;

            if (!ValidAvgRate(course))
            {
                course.AvgRate = 0;
                return;
            }
            course.AvgRate = (course.SumRates / course.TotalReviews);
        }

        private static bool ValidAvgRate(Courses course)
        {
            if (course.TotalReviews <= 0 || course.SumRates <= 0)
            {
                return false;
            }
            return true;
        }

        // <summary>
        // Get total review of course
        // </summary>
        // <param name = "userId" ></ param >
        // < returns ></ returns >
        public async Task<ApiBaseResponse> GetTotalReviewOfUser(Guid userId)
        {
            if (userId == Guid.Empty)
                return new UserIdNullResponse();

            var IsExistEnrolls = await _enrollmentRepository.BuildQuery()
                                                            .FilterByUserId(userId)
                                                            .AnyAsync();
            if (!IsExistEnrolls)
                return new EnrollmentNotForUserFoundResponse(userId);

            var count = await _courseReviewRepository.BuildQuery()
                                                     .FilterByUserId(userId)
                                                     .CountAsync();
            return new ApiOkResponse<int>(count);
        }

        public async Task<Response<int>> GetTotalReviewOfInstructor(Guid userId)
        {
            try
            {
                var IsExistEnrolls = await _enrollmentRepository.BuildQuery()
                                                                .FilterByCourseUserId(userId)
                                                                .AnyAsync();
                if (!IsExistEnrolls)
                    return new Response<int>(true, 0);

                var courses = await _courseReviewRepository.BuildQuery()
                                                           .FilterByUserIdOfCourse(userId)
                                                           .CountAsync();

                return new Response<int>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }

        public async Task<Response<int>> GetTotalReviewOfCourse(Guid courseId)
        {
            try
            {
                var IsExistEnrolls = await _enrollmentRepository.BuildQuery()
                                                                .FilterByCourseId(courseId)
                                                                .AnyAsync();

                if (!IsExistEnrolls)
                    return new Response<int>(true, 0);

                var courses = await _courseReviewRepository.BuildQuery()
                                                           .FilterByCourseId(courseId)
                                                           .CountAsync();
                return new Response<int>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }

        public async Task<Response<float>> GetAVGRatinng(Guid? courseId, Guid? userId)
        {
            try
            {

                if (courseId is null && userId == Guid.Empty)
                    return new Response<float>(false, "must pass userId or CourseId", "400");
                if (courseId != null)
                    userId = null;

                var IsExistEnrolls = await _enrollmentRepository.BuildQuery()
                                                                .FilterByCourseId(courseId)
                                                                .FilterByUserId(userId)
                                                                .AnyAsync();
                if (!IsExistEnrolls)
                    return new Response<float>(true, 0);

                var courses = await _courseReviewRepository.BuildQuery()
                                                           .FilterByCourseId(courseId)
                                                           .FilterByUserId(userId)
                                                           .GetAvgRate();

                return new Response<float>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<float>(false, ex.Message, "404");
            }
        }
        public async Task<Response<List<float>>> GetDetaiRate(Guid? courseId, Guid? userId)
        {
            try
            {

                if (courseId is null && userId == Guid.Empty)
                    return new Response<List<float>>(false, "must pass userId or CourseId", "400");
                if (courseId != null)
                    userId = null;

                var IsExistEnrolls = await _enrollmentRepository.BuildQuery()
                                                               .FilterByCourseId(courseId)
                                                               .FilterByUserId(userId)
                                                               .AnyAsync();
                if (!IsExistEnrolls)
                    return new Response<List<float>>(true, new() { 0, 0, 0, 0, 0 });

                var sumRating = await _courseReviewRepository.BuildQuery()
                                                             .FilterByCourseId(courseId)
                                                             .FilterByUserId(userId)
                                                             .CountAsync();

                if (sumRating == 0)
                {
                    return new Response<List<float>>(true, new() { 0, 0, 0, 0, 0 });
                }

                List<float> rates = new();

                for (var i = 1; i <= 5; i++)
                {
                    rates.Add(await _courseReviewRepository.BuildQuery()
                                                           .FilterByCourseId(courseId)
                                                           .FilterByUserId(userId)
                                                           .FilterByRating(i)
                                                           .GetAvgRatePercent(sumRating));
                }

                return new Response<List<float>>(true, rates);
            }
            catch (Exception ex)
            {
                return new Response<List<float>>(false, ex.Message, null);
            }
        }

        public async Task<Response<BaseResponse>> IsCourseReview(Guid userId, Guid courseId)
        {
            try
            {
                var courseReview = await _courseReviewRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .FilterByCourseId(courseId)
                                                                .AsSelectorAsync(c => _mapper.Map<CourseReviewDTO>(c));

                if (courseReview == null)
                {
                    return new Response<BaseResponse>(false, null);
                }

                return new Response<BaseResponse>(true, null);
            }
            catch (Exception ex)
            {
                return new Response<BaseResponse>(false, ex.Message, null);
            }
        }
        public async Task<Response<CourseReviewDTO>> CheckUserCourseReview(Guid userId, Guid courseId)
        {
            try
            {
                var check = await _courseReviewRepository.BuildQuery()
                                                         .IncludeEnrollment()
                                                         .FilterByUserId(userId)
                                                         .FilterByCourseId(courseId)
                                                         .AnyAsync();

                var review = await _courseReviewRepository.BuildQuery()
                                                         .FilterByUserId(userId)
                                                         .FilterByCourseId(courseId)
                                                         .IncludeEnrollment()
                                                         .AsSelectorAsync(c => _mapper.Map<CourseReviewDTO>(c));

                if (check)
                {
                    return new Response<CourseReviewDTO>(true, review);
                }
                return new Response<CourseReviewDTO>(true, null);
            }
            catch (Exception ex)
            {
                return new Response<CourseReviewDTO>(false, ex.Message, null);
            }
        }
        //
        public async Task<PagedList<CourseReviewDTO>> GetAllCourseReviewOfIntructor(CourseReviewParameters parameters, Guid userId)
        {
            var courseReview = await _courseReviewRepository.BuildQuery()
                                                            .FilterCourseByUSer(userId)
                                                            .FilterByKeyword(parameters.Keyword)
                                                            .IncludeEnrollment()
                                                            .IncludeUser()
                                                            .FilterByRating(parameters.Rating)
                                                            .IncludeCourse()
                                                            .ApplySort(parameters.Orderby)
                                                            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                                            .Take(parameters.PageSize)
                                                            .ToListAsync(cr => _mapper.Map<CourseReviewDTO>(cr));

            var count = await _courseReviewRepository.BuildQuery()
                                                     .FilterCourseByUSer(userId)
                                                     .FilterByKeyword(parameters.Keyword)
                                                     .FilterByRating(parameters.Rating)
                                                     .CountAsync();

            return new PagedList<CourseReviewDTO>(courseReview, count, parameters.PageNumber, parameters.PageSize);
        }
        public async Task<PagedList<CourseReviewDTO>> GetAllCourseReviewOfUser(Guid userId, CourseReviewParameters parameters)
        {
            var courseReview = await _courseReviewRepository.BuildQuery()
                                                            .FilterByUserId(userId)
                                                            .FilterByKeyword(parameters.Keyword)
                                                            .FilterByRating(parameters.Rating)
                                                            .IncludeUser()
                                                            .IncludeCourse()
                                                            .ApplySort(parameters.Orderby)
                                                            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                                            .Take(parameters.PageSize)
                                                            .ToListAsync(c => _mapper.Map<CourseReviewDTO>(c));

            var count = await _courseReviewRepository.BuildQuery()
                                                     .FilterByUserId(userId)
                                                     .FilterByKeyword(parameters.Keyword)
                                                     .FilterByRating(parameters.Rating)
                                                     .CountAsync();
            var pageList = new PagedList<CourseReviewDTO>(courseReview, count, parameters.PageNumber, parameters.PageSize);

            return pageList;
        }
        public async Task<Response<float>> GetAVGRatinngOfIntructor(Guid userId)
        {
            try
            {
                var IsExistEnrolls = await _enrollmentRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .AnyAsync();
                if (!IsExistEnrolls)
                    return new Response<float>(true, 0);

                var courses = await _courseReviewRepository.BuildQuery()
                                                           .FilterCourseByUSer(userId)
                                                           .GetAvgRate();

                return new Response<float>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<float>(false, ex.Message, null);
            }
        }
        public async Task<Response<List<float>>> GetDetaiRateOfIntructor(Guid userId)
        {
            try
            {
                var IsExistEnrolls = await _enrollmentRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .AnyAsync();
                if (!IsExistEnrolls)
                    return new Response<List<float>>(true, new() { 0, 0, 0, 0, 0 });

                var sumRating = await _courseReviewRepository.BuildQuery()
                                                             .FilterCourseByUSer(userId)
                                                             .CountAsync();

                if (sumRating == 0)
                {
                    return new Response<List<float>>(true, new() { 0, 0, 0, 0, 0 });
                }

                List<float> rates = new();

                for (var i = 1; i <= 5; i++)
                {
                    rates.Add(await _courseReviewRepository.BuildQuery()
                                                            //.FilterByUserId(userId)
                                                            .FilterCourseByUSer(userId)
                                                           .FilterByRating(i)
                                                           .GetAvgRatePercent(sumRating));
                }

                return new Response<List<float>>(true, rates);
            }
            catch (Exception ex)
            {
                return new Response<List<float>>(false, ex.Message, null);
            }
        }
    }
}
