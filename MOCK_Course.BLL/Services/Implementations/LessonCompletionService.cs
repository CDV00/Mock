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
    public class LessonCompletionService : ILessonCompletionService
    {
        private readonly ILessonCompletionRepository _lessonCompletionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LessonCompletionService(ILessonCompletionRepository lessonCompletionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _lessonCompletionRepository = lessonCompletionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<BaseResponse>> Add(LessonCompletionRequest lessonCompletionRequest)
        {
            try
            {
                var lessoncompletion = _mapper.Map<LessonCompletion>(lessonCompletionRequest);
                await _lessonCompletionRepository.CreateAsync(lessoncompletion);
                await _unitOfWork.SaveChangesAsync();
                return new Response<BaseResponse>(
                    true, null);
            }
            catch (Exception ex)
            {
                return new Response<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Responses<LessonCompletionResponse>> GetAll(Guid userId)
        {
            try
            {
                var result = await _lessonCompletionRepository.GetAll().Where(s => s.UserId == userId).Include(s => s.User).Include(s => s.Lesson).Include(s => s.User).ToListAsync();

                return new Responses<LessonCompletionResponse>(true, _mapper.Map<IEnumerable<LessonCompletionResponse>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<LessonCompletionResponse>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Remove(Guid IdLessonCompletion)
        {
            try
            {
                var result = await _lessonCompletionRepository.GetByIdAsync(IdLessonCompletion);

                _lessonCompletionRepository.Remove(result);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse { IsSuccess = true };

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }
        public async Task<Response<LessonCompletionResponse>> Update(LessonCompletionUpdateRequest LessonCompletionUpdateRequest)
        {
            try
            {
                var lessoncompletion = _mapper.Map<LessonCompletion>(LessonCompletionUpdateRequest);

                _lessonCompletionRepository.Update(lessoncompletion);
                await _unitOfWork.SaveChangesAsync();
                return new Response<LessonCompletionResponse>(
                    true,
                    _mapper.Map<LessonCompletionResponse>(lessoncompletion)
                );
            }
            catch (Exception ex)
            {
                return new Response<LessonCompletionResponse>(false, ex.Message, null);
            }
        }

    }
}
