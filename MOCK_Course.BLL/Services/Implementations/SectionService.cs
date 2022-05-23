using AutoMapper;
using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepositoty _sectionRepositoty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILessonService _lessonService;
        public SectionService(ISectionRepositoty sectionRepositoty,
            IMapper mapper,
            IUnitOfWork unitOfWork, ILessonService lessonService)
        {
            _sectionRepositoty = sectionRepositoty;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _lessonService = lessonService;
        }
        public async Task<Response<SectionResponse>> Add(SectionRequest sectionRequest)
        {
            try
            {
                var section = _mapper.Map<Section>(sectionRequest);

                await _sectionRepositoty.CreateAsync(section);

                if (sectionRequest.LessonRequests.Count > 0)
                {
                    foreach (var lesionRequest in sectionRequest.LessonRequests)
                    {
                        await _lessonService.Add(lesionRequest);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                return new Response<SectionResponse>(
                    true,
                    _mapper.Map<SectionResponse>(section)
                );
            }
            catch (Exception ex)
            {
                return new Response<SectionResponse>(false, ex.Message, null);
            }
        }

        public async Task<Responses<SectionResponse>> GetAll(Guid courseId)
        {
            try
            {
                var result = await _sectionRepositoty.GetAll().Where(s => s.CourseId == courseId).Include(l => l.Lessons).ToListAsync();

                return new Responses<SectionResponse>(true, _mapper.Map<IEnumerable<SectionResponse>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<SectionResponse>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Remove(Guid idSection)
        {
            try
            {
                var result = await _sectionRepositoty.GetByIdAsync(idSection);

                _sectionRepositoty.Remove(result);
                _unitOfWork.SaveChanges();

                return new BaseResponse { IsSuccess = true };

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<SectionResponse>> Update(SectionUpdateRequest sectionRequest)
        {
            try
            {
                var section = _mapper.Map<Section>(sectionRequest);

                _sectionRepositoty.Update(section);

                if (sectionRequest.LessonRequests.Count > 0)
                {
                    foreach (var lesionRequest in sectionRequest.LessonRequests)
                    {
                        await _lessonService.Update(lesionRequest);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
                return new Response<SectionResponse>(
                    true,
                    _mapper.Map<SectionResponse>(section)
                );
            }
            catch (Exception ex)
            {
                return new Response<SectionResponse>(false, ex.Message, null);
            }
        }
    }
}
