using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.DTO;
using Course.DAL.Models;
using Course.DAL.Repositories;
using System;
using System.Collections.Generic;
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
        public async Task<Response<LessonDTO>> Add(LessonForCreateRequest LessonRequest)
        {
            try
            {
                var Lesson = _mapper.Map<Lesson>(LessonRequest);

                await _LessonRepositoty.CreateAsync(Lesson);
                await _unitOfWork.SaveChangesAsync();
                return new Response<LessonDTO>(
                    true,
                    _mapper.Map<LessonDTO>(Lesson)
                );
            }
            catch (Exception ex)
            {
                return new Response<LessonDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<LessonDTO>> GetAll(Guid sectionId)
        {
            try
            {
                var lesson = await _LessonRepositoty.GetAllBySectionId(sectionId);

                return new Responses<LessonDTO>(true, _mapper.Map<IEnumerable<LessonDTO>>(lesson));
            }
            catch (Exception ex)
            {
                return new Responses<LessonDTO>(false, ex.Message, null);
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

        public async Task<Response<LessonDTO>> Update(Guid id, LessonForUpdateRequest LessonRequest)
        {
            try
            {
                var lesson = _LessonRepositoty.GetByIdAsync(id);

                if (lesson is null)
                {
                    return new Response<LessonDTO>(false, null, "can't find lesson");
                }
                await _mapper.Map(LessonRequest, lesson);
                await _unitOfWork.SaveChangesAsync();

                var lessonResponse = _mapper.Map<LessonDTO>(LessonRequest);
                return new Response<LessonDTO>(
                    true,
                   lessonResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<LessonDTO>(false, ex.Message, null);
            }
        }
    }
}
