using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;
using Course.DAL.Models;
using Course.DAL.Repositories;
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
        public async Task<Response<LessonResponse>> Add(LessonCreateRequest LessonRequest)
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
                var lesson = await _LessonRepositoty.GetByIdAsync(idLesson);
                if (lesson is null)
                {
                    return new BaseResponse(false, null, "can't find lesson");
                }
                _LessonRepositoty.Remove(lesson);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<LessonResponse>> Update(Guid id, LessonUpdateRequest LessonRequest)
        {
            try
            {
                var lesson = _LessonRepositoty.GetByIdAsync(id);
                if (lesson is null)
                {
                    return new Response<LessonResponse>(false, null, "can't find lesson");
                }
                await _mapper.Map(LessonRequest, lesson);
                await _unitOfWork.SaveChangesAsync();

                var lessonResponse = _mapper.Map<LessonResponse>(LessonRequest);
                return new Response<LessonResponse>(
                    true,
                   lessonResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<LessonResponse>(false, ex.Message, null);
            }
        }
    }
}
