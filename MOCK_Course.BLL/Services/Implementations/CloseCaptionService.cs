using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
{
    public class CloseCaptionService : ICloseCaptionService
    {
        private readonly ICloseCaptionRepository _CloseCaptionRepositoty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CloseCaptionService(ICloseCaptionRepository CloseCaptionRepositoty,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _CloseCaptionRepositoty = CloseCaptionRepositoty;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<CloseCaptionDTO>> Add(CloseCaptionForCreateRequest CloseCaptionRequest, Guid courseId)
        {
            try
            {
                var CloseCaption = _mapper.Map<CloseCaption>(CloseCaptionRequest);
                CloseCaption.Id = courseId;

                await _CloseCaptionRepositoty.CreateAsync(CloseCaption);
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

    }
}
