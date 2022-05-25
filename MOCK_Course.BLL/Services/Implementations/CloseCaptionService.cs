using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Responsesnamespace;
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

        public async Task<BaseResponse> RemoveAll(Guid courseId)
        {
            try
            {
                var audioLanguages = await _CloseCaptionRepositoty.GetAll().Where(a => a.CourseId == courseId).ToListAsync();

                if (audioLanguages.Count == 0)
                    return new Response<BaseResponse>(false, "don't have any close caption", null);

                foreach (var item in audioLanguages)
                {
                    _CloseCaptionRepositoty.Remove(item);
                }

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }
    }
}
