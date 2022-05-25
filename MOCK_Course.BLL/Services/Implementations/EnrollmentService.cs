using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Course.BLL.Responsesnamespace;

namespace Course.BLL.Services.Implementations
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Add(EnrollmentRequest enrollmentRequest)
        {
            try
            {
                var enrollment = _mapper.Map<Enrollment>(enrollmentRequest);
                await _enrollmentRepository.CreateAsync(enrollment);
                await _unitOfWork.SaveChangesAsync();
                return new Response<BaseResponse>(
                    true, null);
            }
            catch (Exception ex)
            {
                return new Response<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> IsEnrollmented(EnrollmentRequest enrollmentRequest)
        {
               if(await _enrollmentRepository.FindByCondition(l=>l.UserId == enrollmentRequest.UserId && l.CourseId == enrollmentRequest.CourseId).FirstOrDefaultAsync() == null)
            {
                return new BaseResponse(false);
            }
            return new BaseResponse(true);
        }

        //public async Task<Responses<EnrollmentResponse>> GetAll(Guid userId)
        //{
        //    try
        //    {
        //        var result = await _enrollmentRepository.GetAll().Where(s => s.UserId == userId).Include(s => s.User).Include(s => s.Courses).Include(s => s.User).Include(s => s.Courses.Category).ToListAsync();

        //        return new Responses<EnrollmentResponse>(true, _mapper.Map<IEnumerable<EnrollmentResponse>>(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<EnrollmentResponse>(false, ex.Message, null);
        //    }
        //}

        //public async Task<BaseResponse> Remove(Guid enrollmentId)
        //{
        //    try
        //    {
        //        var result = await _enrollmentRepository.GetByIdAsync(enrollmentId);

        //        _enrollmentRepository.Remove(result);
        //        await _unitOfWork.SaveChangesAsync();

        //        return new BaseResponse { IsSuccess = true };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<BaseResponse>(false, ex.Message, null);
        //    }
        //}
        //public async Task<Response<EnrollmentResponse>> Update(EnrollmentUpdateRequest enrollmentUpdateRequest)
        //{
        //    try
        //    {
        //        var enrollment = _mapper.Map<Enrollment>(enrollmentUpdateRequest);

        //        _enrollmentRepository.Update(enrollment);
        //        await _unitOfWork.SaveChangesAsync();
        //        return new Response<EnrollmentResponse>(
        //            true,
        //            _mapper.Map<EnrollmentResponse>(enrollment)
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<EnrollmentResponse>(false, ex.Message, null);
        //    }
        //}

    }
}
