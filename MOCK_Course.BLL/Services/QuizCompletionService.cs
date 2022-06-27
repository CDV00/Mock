using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.DTO;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Course.BLL.Responses;
using Entities.DTOs;
using Repository.Repositories.Abstraction;

namespace Course.BLL.Services
{
    public class QuizCompletionService : IQuizCompletionService
    {
        private readonly IQuizCompletionRepository _quizCompletionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuizCompletionService(IQuizCompletionRepository quizCompletionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _quizCompletionRepository = quizCompletionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<QuizCompletionDTO>> Add(Guid userId, QuizCompletionRequest lectureCompletionRequest)
        {
            if (await _quizCompletionRepository.BuildQuery()
                                               .FilterByQuiz(lectureCompletionRequest.QuizId)
                                               .FilterByUser(userId)
                                               .AnyAsync())
                return new Response<QuizCompletionDTO>(false, $"Duplicate quiz completion with quiz id:{lectureCompletionRequest.QuizId}", "400");

            var quizcompletion = _mapper.Map<QuizCompletion>(lectureCompletionRequest);

            quizcompletion.UserId = userId;

            await _quizCompletionRepository.CreateAsync(quizcompletion);
            await _unitOfWork.SaveChangesAsync();

            return new Response<QuizCompletionDTO>(true, _mapper.Map<QuizCompletionDTO>(quizcompletion));
        }


        public async Task<BaseResponse> IsCompletion(Guid userId, Guid lectureId)
        {
            try
            {
                QuizCompletionRequest lectureCompletionRequest = new QuizCompletionRequest();
                lectureCompletionRequest.QuizId = lectureId;
                var lectureCompletion = _mapper.Map<QuizCompletion>(lectureCompletionRequest);
                lectureCompletion.UserId = userId;

                var Result = await _quizCompletionRepository.IsCompletion(lectureCompletion);
                return new BaseResponse(Result);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }
























        //public async Task<Response<QuizCompletionDTO>> Update(Guid userId, QuizCompletionRequest lectureCompletionRequest)
        //{
        //    var lecureCompletion = await _quizCompletionRepository.BuildQuery()
        //                                                            .FilterByQuiz(lectureCompletionRequest.QuizId)
        //                                                            .FilterByUser(userId)
        //                                                            .AsSelectorAsync(l => l);
        //    if (lecureCompletion == null)
        //        return new Response<QuizCompletionDTO>(false, $"Don't have lecture completion with lecture id:{lectureCompletionRequest.QuizId}", "400");

        //    _mapper.Map(lectureCompletionRequest, lecureCompletion);

        //    _quizCompletionRepository.Update(lecureCompletion);
        //    await _unitOfWork.SaveChangesAsync();

        //    return new Response<QuizCompletionDTO>(true, _mapper.Map<QuizCompletionDTO>(lecureCompletion));
        //}
    }
}
