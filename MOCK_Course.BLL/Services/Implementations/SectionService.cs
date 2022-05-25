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
    public class SectionService : ISectionService
    {
        private readonly ISectionRepositoty _sectionRepositoty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SectionService(ISectionRepositoty sectionRepositoty,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _sectionRepositoty = sectionRepositoty;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<SectionResponse>> Add(SectionCreateRequest sectionRequest)
        {
            try
            {
                var section = _mapper.Map<Section>(sectionRequest);

                await _sectionRepositoty.CreateAsync(section);
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
                var result = await _sectionRepositoty.GetAll().Where(s => s.CourseId == courseId).ToListAsync();

                return new Responses<SectionResponse>(true, _mapper.Map<IEnumerable<SectionResponse>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<SectionResponse>(false, ex.Message, null);
            }
        }

        public async Task<Responsesnamespace.BaseResponse> Remove(Guid idSection)
        {
            try
            {
                var result = await _sectionRepositoty.GetByIdAsync(idSection);

                _sectionRepositoty.Remove(result);
                _unitOfWork.SaveChanges();

                return new BaseResponse(true);

            }
            catch (Exception ex)
            {
                return new Responses<Responsesnamespace.BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<SectionResponse>> Update(SectionUpdateRequest sectionRequest)
        {
            try
            {
                var section = _mapper.Map<Section>(sectionRequest);

                _sectionRepositoty.Update(section);
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
