using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.DAL.Models;
using Course.DAL.Repositories;
using System;
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
        public async Task<Response<CloseCaptionCreateResponse>> Add(CloseCaptionCreateRequest CloseCaptionRequest, Guid courseId)
        {
            try
            {
                var CloseCaption = _mapper.Map<CloseCaption>(CloseCaptionRequest);
                CloseCaption.CourseId = courseId;

                await _CloseCaptionRepositoty.CreateAsync(CloseCaption);
                await _unitOfWork.SaveChangesAsync();

                return new Response<CloseCaptionCreateResponse>(
                    true,
                    _mapper.Map<CloseCaptionCreateResponse>(CloseCaption)
                );
            }
            catch (Exception ex)
            {
                return new Response<CloseCaptionCreateResponse>(false, ex.Message, null);
            }
        }
    }
}
