﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Course.BLL.Responsesnamespace;

namespace Course.BLL.Services.Implementations
{
    public class CourseCompletionService : ICourseCompletionService
    {
        private readonly ICourseCompletionRepository _courseCompletionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseCompletionService(ICourseCompletionRepository courseCompletionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _courseCompletionRepository = courseCompletionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Add(CourseCompletionRequest courseCompletionRequest)
        {
            try
            {
                var coursecompletion = _mapper.Map<CourseCompletion>(courseCompletionRequest);

                await _courseCompletionRepository.CreateAsync(coursecompletion);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(
                    true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> IsCompletion(CourseCompletionRequest courseCompletionRequest)
        {
            if (await _courseCompletionRepository.FindByCondition(l => l.CourseId == courseCompletionRequest.CourseId && l.UserId == courseCompletionRequest.UserId).FirstOrDefaultAsync() == null)
            {
                return new BaseResponse(false);
            }
            return new BaseResponse(true);
        }

        //public async Task<Responses<CourseCompletionResponse>> GetAll(Guid userId)
        //{
        //    try
        //    {
        //        var result = await _courseCompletionRepository.GetAll().Where(s => s.UserId == userId).Include(s => s.User).Include(s => s.Course).Include(s => s.User).Include(s => s.Course.Category).ToListAsync();

        //        return new Responses<CourseCompletionResponse>(true, _mapper.Map<IEnumerable<CourseCompletionResponse>>(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<CourseCompletionResponse>(false, ex.Message, null);
        //    }
        //}

        //public async Task<BaseResponse> Remove(Guid IdCourseCompletion)
        //{
        //    try
        //    {
        //        var result = await _courseCompletionRepository.GetByIdAsync(IdCourseCompletion);

        //        _courseCompletionRepository.Remove(result);
        //        await _unitOfWork.SaveChangesAsync();

        //        return new BaseResponse { IsSuccess = true };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<BaseResponse>(false, ex.Message, null);
        //    }
        //}
        //public async Task<Response<CourseCompletionResponse>> Update(CourseCompletionUpdateRequest courseCompletionUpdateRequest)
        //{
        //    try
        //    {
        //        var coursecompletion = _mapper.Map<CourseCompletion>(courseCompletionUpdateRequest);

        //        _courseCompletionRepository.Update(coursecompletion);
        //        await _unitOfWork.SaveChangesAsync();
        //        return new Response<CourseCompletionResponse>(
        //            true,
        //            _mapper.Map<CourseCompletionResponse>(coursecompletion)
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<CourseCompletionResponse>(false, ex.Message, null);
        //    }
        //}

    }
}