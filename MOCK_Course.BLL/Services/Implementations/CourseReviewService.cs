using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
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
        public async Task<Responses<CourseReviewResponse>> GetAll(Guid courseId)
        {
            try
            {
                var result = await _courseReviewRepository.GetAll().Where(c => c.Enrollment.CourseId == courseId).ToListAsync();

                return new Responses<CourseReviewResponse>(true, _mapper.Map<IEnumerable<CourseReviewResponse>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<CourseReviewResponse>(false, ex.Message, null);
            }

        }

        public async Task<Response<CourseReviewResponse>> Add(CourseReviewRequest courseReviewRequest)
        {
            try
            {
                var courseReview = _mapper.Map<CourseReview>(courseReviewRequest);

                await _courseReviewRepository.CreateAsync(courseReview);
                await _unitOfWork.SaveChangesAsync();
                return new Response<CourseReviewResponse>(true, _mapper.Map<CourseReviewResponse>(courseReview));
            }
            catch (Exception ex)
            {
                return new Response<CourseReviewResponse>(false, ex.Message, null);
            }
        }
        /// <summary>
        /// update course review
        /// </summary>
        /// <param name="courseReviewUpdateRequest"></param>
        /// <returns></returns>
        public async Task<Response<CourseReviewResponse>> Update(Guid id, CourseReviewUpdateRequest courseReviewUpdateRequest)
        {
            try
            {
                var courseReview = await _courseReviewRepository.GetByIdAsync(id);
                //check coursereview null
                _mapper.Map(courseReviewUpdateRequest, courseReview);

                await _unitOfWork.SaveChangesAsync();
                return new Response<CourseReviewResponse>(
                    true,
                    _mapper.Map<CourseReviewResponse>(courseReview)
                );
            }
            catch (Exception ex)
            {
                return new Response<CourseReviewResponse>(false, ex.Message, null);
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
    }
}
