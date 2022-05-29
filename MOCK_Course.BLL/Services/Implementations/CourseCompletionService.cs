using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Course.BLL.DTO;

namespace Course.BLL.Services.Implementations
{
    public class CourseCompletionService : ICourseCompletionService
    {
        private readonly ICourseCompletionRepository _courseCompletionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseCompletionService(ICourseCompletionRepository courseCompletionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _courseCompletionRepository = courseCompletionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Add(Guid userId, CourseCompletionRequest courseCompletionRequest)
        {
            try
            {
                var coursecompletion = _mapper.Map<CourseCompletion>(courseCompletionRequest);
                coursecompletion.UserId = userId;

                await _courseCompletionRepository.CreateAsync(coursecompletion);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(
                    true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }
    }
}
