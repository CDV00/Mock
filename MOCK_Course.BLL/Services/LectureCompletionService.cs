using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.DTO;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Course.BLL.Responses;

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

        public async Task<Response<LectureCompletionDTO>> Add(Guid userId, LectureCompletionRequest lectureCompletionRequest)
        {
            if (await _lessonCompletionRepository.BuildQuery()
                                                 .FilterByLecture(lectureCompletionRequest.LectureId)
                                                 .FilterByUser(userId)
                                                 .AnyAsync())
                return new Response<LectureCompletionDTO>(false, $"Duplicate lecture completion with lecture id:{lectureCompletionRequest.LectureId}", "400");

            var lessoncompletion = _mapper.Map<LectureCompletion>(lectureCompletionRequest);

            lessoncompletion.UserId = userId;

            await _lessonCompletionRepository.CreateAsync(lessoncompletion);
            await _unitOfWork.SaveChangesAsync();

            return new Response<LectureCompletionDTO>(true, _mapper.Map<LectureCompletionDTO>(lessoncompletion));
        }

        public async Task<Response<LectureCompletionDTO>> Update(Guid userId, LectureCompletionRequest lectureCompletionRequest)
        {
            var lecureCompletion = await _lessonCompletionRepository.BuildQuery()
                                                                    .FilterByLecture(lectureCompletionRequest.LectureId)
                                                                    .FilterByUser(userId)
                                                                    .AsSelectorAsync(l => l);
            if (lecureCompletion == null)
                return new Response<LectureCompletionDTO>(false, $"Don't have lecture completion with lecture id:{lectureCompletionRequest.LectureId}", "400");

            _mapper.Map(lectureCompletionRequest, lecureCompletion);

            _lessonCompletionRepository.Update(lecureCompletion);
            await _unitOfWork.SaveChangesAsync();

            return new Response<LectureCompletionDTO>(true, _mapper.Map<LectureCompletionDTO>(lecureCompletion));
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

        public async Task<int> TotalLectureCompletionBySection(Guid userId, Guid sectionId)
        {
            var count = await _lessonCompletionRepository.BuildQuery()
                                                         .FilterLectureCompletion(userId, sectionId)
                                                         .CountAsync();
            return count;
        }
        public async Task<int> TotalLectureCompletionBycourse(Guid userId, Guid courseId)
        {
            var count = await _lessonCompletionRepository.BuildQuery()
                                                         .FilterLectureCompletionCourse(userId, courseId)
                                                         .CountAsync();
            return count;
        }
    }
}
