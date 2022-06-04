using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.DTO;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.BLL.Responses;

namespace Course.BLL.Services.Implementations
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepositoty _sectionRepositoty;
        private readonly ILectureRepository _lessonRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SectionService(ISectionRepositoty sectionRepositoty,
            IMapper mapper,
            IUnitOfWork unitOfWork, ILectureRepository lessonRepository)
        {
            _sectionRepositoty = sectionRepositoty;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _lessonRepository = lessonRepository;
        }

        public async Task<Response<int>> GetTotal(Guid courseId)
        {
            try
            {

                var count = await _sectionRepositoty.BuildQuery()
                                        .FilterByCourseId(courseId)
                                        .CountAsync();

                return new Response<int>(
                    true,
                    _mapper.Map<int>(count)
                );
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }
        public async Task<Response<SectionDTO>> Add(SectionCreateRequest sectionRequest)
        {
            try
            {
                var section = _mapper.Map<Section>(sectionRequest);

                await _sectionRepositoty.CreateAsync(section);
                await _unitOfWork.SaveChangesAsync();

                return new Response<SectionDTO>(
                    true,
                    _mapper.Map<SectionDTO>(section)
                );
            }
            catch (Exception ex)
            {
                return new Response<SectionDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<SectionDTO>> GetAll(Guid courseId)
        {
            try
            {
                var result = await _sectionRepositoty.GetAllByCourseId(courseId);

                return new Responses<SectionDTO>(true, _mapper.Map<IEnumerable<SectionDTO>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<SectionDTO>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Remove(Guid idSection)
        {
            try
            {
                var section = await _sectionRepositoty.GetByIdAsync(idSection);
                if (section is null)
                {
                    return new BaseResponse(false, null, "can't find lesson");
                }

                await _lessonRepository.RemoveBySectionId(idSection);
                _sectionRepositoty.Remove(section);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse(true);

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<SectionDTO>> Update(Guid id, SectionUpdateRequest sectionRequest)
        {
            try
            {

                var section = await _sectionRepositoty.GetByIdAsync(id);

                if (section is null)
                {
                    return new Response<SectionDTO>(false, null, "can't find section");
                }

                _mapper.Map(sectionRequest, section);
                await _unitOfWork.SaveChangesAsync();

                var sectionResponse = _mapper.Map<SectionDTO>(section);

                return new Response<SectionDTO>(
                    true,
                    _mapper.Map<SectionDTO>(sectionResponse)
                );
            }
            catch (Exception ex)
            {
                return new Response<SectionDTO>(false, ex.Message, null);
            }
        }
    }
}
