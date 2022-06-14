using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;

namespace Course.BLL.Services
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


        public async Task<BaseResponse> IsCompletion(Guid userId, Guid courseId)
        {
            try
            {
                var courseCompletion = await _courseCompletionRepository.FindByCondition(c => c.CourseId == courseId && c.UserId == userId)
                                                                        .FirstOrDefaultAsync();

                if (courseCompletion == null)
                    return new BaseResponse(false);

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Add(Guid userId, Guid courseId)
        {
            try
            {
                var coursecompletion = new CourseCompletion
                {
                    UserId = userId,
                    CourseId = courseId,
                };

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
