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

namespace Course.BLL.Services
{
    public class CourseReviewService : ICourseReviewService
    {
        private readonly ICousesRepository _cousesRepository;
        private readonly ICourseReviewRepository _courseReviewRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// contructer CourseReviewService 
        /// </summary>
        /// <param name="courseReviewRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public CourseReviewService(ICourseReviewRepository courseReviewRepository, ICousesRepository cousesRepository, IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<PagedList<CourseReviewDTO>> GetAll(Guid courseId, CourseReviewParameters courseReviewParameters)
        {
            var courseReview = await _courseReviewRepository.BuildQuery()
                                                            .FilterByCourseId(courseId)
                                                            .FilterByKeyword(courseReviewParameters.Keyword)
                                                            .IncludeUser()
                                                            .ApplySort(courseReviewParameters.Orderby)
                                                            .Skip((courseReviewParameters.PageNumber - 1) * courseReviewParameters.PageSize)
                                                            .Take(courseReviewParameters.PageSize)
                                                            .ToListAsync(c => _mapper.Map<CourseReviewDTO>(c));

            var count = await _courseReviewRepository.BuildQuery()
                                                     .FilterByCourseId(courseId)
                                                     .CountAsync();
            var pageList = new PagedList<CourseReviewDTO>(courseReview, count, courseReviewParameters.PageNumber, courseReviewParameters.PageSize);

            return pageList;
        }

        public async Task<Response<CourseReviewDTO>> Add(CourseReviewRequest courseReviewRequest)
        {
            try
            {
                var courseReview = _mapper.Map<CourseReview>(courseReviewRequest);
                courseReview.CreatedAt = DateTime.Now;
                await _courseReviewRepository.CreateAsync(courseReview);

                var enrollment = await _enrollmentRepository.GetByIdAsync(courseReviewRequest.EnrollmentId);
                var course = await _cousesRepository.GetByIdAsync(enrollment.CourseId);
                course.SumRates += courseReview.Rating;
                course.TotalReviews++;
                course.AvgRate = course.SumRates / course.TotalReviews;

                await _unitOfWork.SaveChangesAsync();
                return new Response<CourseReviewDTO>(true, _mapper.Map<CourseReviewDTO>(courseReview));
            }
            catch (Exception ex)
            {
                return new Response<CourseReviewDTO>(false, ex.Message, null);
            }
        }
        /// <summary>
        /// update course review
        /// </summary>
        /// <param name="courseReviewUpdateRequest"></param>
        /// <returns></returns>
        public async Task<Response<CourseReviewDTO>> Update(Guid id, CourseReviewUpdateRequest courseReviewUpdateRequest)
        {
            try
            {
                var courseReview = await _courseReviewRepository.BuildQuery()
                                                                .GetById(id)
                                                                .IncludeEnrollment()
                                                                .AsSelectorAsync(c => c);

                if (courseReview == null)
                    return new Response<CourseReviewDTO>(true, "Don't have this reviews", null);

                courseReview.UpdatedAt = DateTime.Now;

                var course = await _cousesRepository.GetByIdAsync(courseReview.Enrollment.CourseId);
                course.SumRates -= courseReview.Rating;
                course.SumRates += courseReviewUpdateRequest.Rating;
                course.AvgRate = course.SumRates / course.TotalReviews;

                _mapper.Map(courseReviewUpdateRequest, courseReview);
                await _unitOfWork.SaveChangesAsync();
                return new Response<CourseReviewDTO>(
                    true,
                    _mapper.Map<CourseReviewDTO>(courseReview)
                );
            }
            catch (Exception ex)
            {
                return new Response<CourseReviewDTO>(false, ex.Message, null);
            }
        }
        /// <summary>
        /// delete course review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseResponse> Delete(Guid id)
        {
            try
            {
                var courseReview = await _courseReviewRepository.BuildQuery()
                                                                .GetById(id)
                                                                .IncludeEnrollment()
                                                                .AsSelectorAsync(c => c);
                if (courseReview == null)
                    return new BaseResponse(true, "Don't have this reviews", null);

                var course = await _cousesRepository.GetByIdAsync(courseReview.Enrollment.CourseId);
                course.SumRates -= courseReview.Rating;
                course.TotalReviews--;
                course.AvgRate = course.SumRates / course.TotalReviews;

                _courseReviewRepository.Remove(courseReview, true);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Delete success", null);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }
        // <summary>
        // Get total review of course
        // </summary>
        // <param name = "userId" ></ param >
        // < returns ></ returns >
        public async Task<Response<int>> GetTotalReviewOfUser(Guid userId)
        {
            try
            {
                var IsExistEnrolls = await _enrollmentRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .AnyAsync();
                if (!IsExistEnrolls)
                    return new Response<int>(true, 0);

                var courses = await _courseReviewRepository.BuildQuery()
                                                           .FilterByUserId(userId)
                                                           .CountAsync();
                return new Response<int>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
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
                return new Response<float>(false, ex.Message, null);
            }
        }
        public async Task<Response<List<float>>> GetDetaiRate(Guid? courseId, Guid? userId)
        {
            try
            {
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

        public async Task<BaseResponse> IsCourseReview(Guid userId, Guid courseId)
        {
            try
            {
                var courseReview = await _courseReviewRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .FilterByCourseId(courseId)
                                                                .AsSelectorAsync(c => _mapper.Map<CourseReviewDTO>(c));

                if (courseReview == null)
                {
                    return new BaseResponse(false);
                }

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
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
                                                         .AsSelectorAsync(c =>                                                           _mapper.Map<CourseReviewDTO>(c));

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
        public async Task<PagedList<CourseReviewDTO>> GetAllCourseReviewOfIntructor(RequestParameters requestParameters, Guid userId)
        {
            var courseReview = await _courseReviewRepository.BuildQuery()
                                                             .FilterCourseByUSer(userId)
                                                             .IncludeEnrollment()
                                                             .IncludeUser()
                                                             .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
                                                             .Take(requestParameters.PageSize)
                                                             .ToListAsync(cr => _mapper.Map<CourseReviewDTO>(cr));
            var count = courseReview.Count;
            return new PagedList<CourseReviewDTO>(courseReview, count, requestParameters.PageNumber, requestParameters.PageSize);
        }
    }
}
