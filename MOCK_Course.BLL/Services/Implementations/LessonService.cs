using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Course.DAL.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _LessonRepositoty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LessonService(ILessonRepository LessonRepositoty,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _LessonRepositoty = LessonRepositoty;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<LessonResponse>> Add(LessonRequest LessonRequest)
        {
            try
            {
                var Lesson = _mapper.Map<Lesson>(LessonRequest);

                await _LessonRepositoty.CreateAsync(Lesson);
                await _unitOfWork.SaveChangesAsync();
                return new Response<LessonResponse>(
                    true,
                    _mapper.Map<LessonResponse>(Lesson)
                );
            }
            catch (Exception ex)
            {
                return new Response<LessonResponse>(false, ex.Message, null);
            }
        }

        public async Task<Responses<LessonResponse>> GetAll(Guid sectionId)
        {
            try
            {
                var result = await _LessonRepositoty.GetAll().Where(s => s.SectionId == sectionId).ToListAsync();
                return new Responses<LessonResponse>(true, _mapper.Map<IEnumerable<LessonResponse>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<LessonResponse>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Remove(Guid idLesson)
        {
            try
            {
                var result = await _LessonRepositoty.GetByIdAsync(idLesson);
                _LessonRepositoty.Remove(result);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<LessonResponse>> Update(LessonUpdateRequest LessonRequest)
        {
            try
            {
                if (_LessonRepositoty.GetById(LessonRequest.Id) == null)
                    return new Response<LessonResponse>(false, "can't find this lesson",null);

                var Lesson = _mapper.Map<Lesson>(LessonRequest);

                _LessonRepositoty.Update(Lesson);
                await _unitOfWork.SaveChangesAsync();
                return new Response<LessonResponse>(
                    true,
                    _mapper.Map<LessonResponse>(Lesson)
                );
            }
            catch (Exception ex)
            {
                return new Response<LessonResponse>(false, ex.Message, null);
            }
        }
    }
}
