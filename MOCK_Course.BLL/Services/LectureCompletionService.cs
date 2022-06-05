using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.DTO;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;

namespace Course.BLL.Services
{
    public class LectureCompletionService : ILectureCompletionService
    {
        private readonly ILectureCompletionRepository _lessonCompletionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LectureCompletionService(ILectureCompletionRepository lessonCompletionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _lessonCompletionRepository = lessonCompletionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Add(Guid userId, LectureCompletionRequest lessonCompletionRequest)
        {
            try
            {
                var lessoncompletion = _mapper.Map<LectureCompletion>(lessonCompletionRequest);

                lessoncompletion.UserId = userId;
                await _lessonCompletionRepository.CreateAsync(lessoncompletion);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new Response<BaseResponse>(false, ex.Message, null);
            }
        }
    }
}
