using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;

namespace Course.BLL.Services
{
    public class CloseCaptionService : ICloseCaptionService
    {
        private readonly ICloseCaptionRepository _closeCaptionRepositoty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CloseCaptionService(ICloseCaptionRepository CloseCaptionRepositoty,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _closeCaptionRepositoty = CloseCaptionRepositoty;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<CloseCaptionDTO>> Add(CloseCaptionForCreateRequest CloseCaptionRequest, Guid courseId)
        {
            try
            {
                var CloseCaption = _mapper.Map<CloseCaption>(CloseCaptionRequest);
                CloseCaption.Id = courseId;

                await _closeCaptionRepositoty.CreateAsync(CloseCaption);
                await _unitOfWork.SaveChangesAsync();

                return new Response<CloseCaptionDTO>(
                    true,
                    _mapper.Map<CloseCaptionDTO>(CloseCaption)
                );
            }
            catch (Exception ex)
            {
                return new Response<CloseCaptionDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<CloseCaptionDTO>> GetAll()
        {
            try
            {

                var closeCaption = await _closeCaptionRepositoty.GetAll().ToListAsync();

                return new Responses<CloseCaptionDTO>(
                    true,
                    _mapper.Map<IList<CloseCaptionDTO>>(closeCaption)
                );
            }
            catch (Exception ex)
            {
                return new Responses<CloseCaptionDTO>(false, ex.Message, null);
            }
        }
    }
}
