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
                lessoncompletion.CreatedAt = DateTime.Now;

                await _lessonCompletionRepository.CreateAsync(lessoncompletion);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new Response<BaseResponse>(false, ex.Message, null);
            }
        }
        public async Task<BaseResponse> IsCompletion(Guid userId, Guid lectureId)
        {
            try
            {
                LectureCompletionRequest lectureCompletionRequest = new LectureCompletionRequest();
                lectureCompletionRequest.LectureId = lectureId;
                var lectureCompletion = _mapper.Map<LectureCompletion>(lectureCompletionRequest);
                lectureCompletion.UserId = userId;

                var Result = await _lessonCompletionRepository.IsCompletion(lectureCompletion);
                return new BaseResponse(Result);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }

        //Lấy tí lệ khóa học đã hoàn thành; tong lecture * 100%/cac khoa hoc hoan thanh
        public async Task<int> totalLectureCompletionBySection(Guid userId, Guid sectionId)
        {
            var count = await _lessonCompletionRepository.BuildQuery()
                                                         .FilterLectureCompletion(userId, sectionId)
                                                         .CountAsync();
            return count;
        }
        public async Task<int> totalLectureCompletionBycourse(Guid userId, Guid courseId)
        {
            var count = await _lessonCompletionRepository.BuildQuery()
                                                         .FilterLectureCompletionCourse(userId, courseId)
                                                         .CountAsync();
            return count;
        }
    }
}
