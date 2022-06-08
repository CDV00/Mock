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
using System.Linq;
using Course.DAL.DTOs;
using System.Collections;

namespace Course.BLL.Services
{
    public class CourseReviewService : ICourseReviewService
    {
        private readonly ICourseReviewRepository _courseReviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        /// <summary>
        /// contructer CourseReviewService 
        /// </summary>
        /// <param name="courseReviewRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public CourseReviewService(ICourseReviewRepository courseReviewRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _courseReviewRepository = courseReviewRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// get all course review
        /// </summary>
        /// <returns></returns>
        public async Task<Responses<CourseReviewDTO>> GetAll(Guid courseId)
        {
            try
            {
                var courseReview = await _courseReviewRepository.BuildQuery()
                                                          .FilterByCourseId(courseId)
                                                          .IncludeUser()
                                                          .ToListAsync(c => _mapper.Map<CourseReviewDTO>(c));

                return new Responses<CourseReviewDTO>(true, _mapper.Map<IEnumerable<CourseReviewDTO>>(courseReview));
            }
            catch (Exception ex)
            {
                return new Responses<CourseReviewDTO>(false, ex.Message, null);
            }
        }

        public async Task<Response<CourseReviewDTO>> Add(CourseReviewRequest courseReviewRequest)
        {
            try
            {
                var courseReview = _mapper.Map<CourseReview>(courseReviewRequest);

                await _courseReviewRepository.CreateAsync(courseReview);
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
                var courseReview = await _courseReviewRepository.GetByIdAsync(id);
                //check coursereview null
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
                var courseReview = await _courseReviewRepository.GetByIdAsync(id);
                // Check courseReview null

                _courseReviewRepository.Remove(courseReview);
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
        public async Task<Response<int>> GetTotal(Guid userId)
        {
            try
            {
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
        public async Task<Response<float>> GetAVGRatinng(Guid courseId)
        {
            try
            {
                var courses = await _courseReviewRepository.BuildQuery()
                                                           .FilterByCourseId(courseId)
                                                           .GetAvgRate();
                return new Response<float>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<float>(false, ex.Message, null);
            }
        }
        public async Task<Response<RatingDetailDTO>> GetDetaiRate(Guid courseId)
        {
            try
            {
                var sumRating = await _courseReviewRepository.BuildQuery().
                    FilterByCourseId(courseId).SumAsync(c => (long)c.Rating);

                /*var result = await _courseReviewRepository.BuildQuery()
                                                           .FilterByCourseId(courseId)
                                                           .FilterByRating(rating)
                                                           .GetAvgRatePercent(sumRating);*/
                var result = new RatingDetailDTO()
                {
                    OneRatingPercent = await _courseReviewRepository.BuildQuery()
                                                           .FilterByCourseId(courseId)
                                                           .FilterByRating(1)
                                                           .GetAvgRatePercent(sumRating),
                    TwoRatingPercent = await _courseReviewRepository.BuildQuery()
                                                           .FilterByCourseId(courseId)
                                                           .FilterByRating(2)
                                                           .GetAvgRatePercent(sumRating),
                    ThreeRatingPercent = await _courseReviewRepository.BuildQuery()
                                                           .FilterByCourseId(courseId)
                                                           .FilterByRating(3)
                                                           .GetAvgRatePercent(sumRating),
                    FourRatingPercent = await _courseReviewRepository.BuildQuery()
                                                           .FilterByCourseId(courseId)
                                                           .FilterByRating(4)
                                                           .GetAvgRatePercent(sumRating),
                    FiveRatingPercent = await _courseReviewRepository.BuildQuery()
                                                           .FilterByCourseId(courseId)
                                                           .FilterByRating(5)
                                                           .GetAvgRatePercent(sumRating)
                };


                return new Response<RatingDetailDTO>(true, result);
            }
            catch (Exception ex)
            {
                return new Response<RatingDetailDTO>(false, ex.Message, null);
            }
        }
        /*public async Task<Responses<float>> GetRate(Guid courseId)
        {
            try
            {
                var sumRating = await _courseReviewRepository.BuildQuery().
                    FilterByCourseId(courseId).SumAsync(c => (long)c.Rating);

                List<float> rates = new List<float>();

                for (int i = 0; i < 5; i++)
                {
                    rates[i] = await _courseReviewRepository.BuildQuery()
                                                          .FilterByCourseId(courseId)
                                                          .FilterByRating(++i)
                                                          .GetAvgRatePercent(sumRating);

                }

                var result =  rates.AsEnumerable();

                return new Responses<float>(true, result);
            }
            catch (Exception ex)
            {
                return new Responses<float>(false, ex.Message, null);
            }
        }*/

    }
}
