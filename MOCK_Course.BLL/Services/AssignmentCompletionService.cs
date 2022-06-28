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
using Microsoft.EntityFrameworkCore;

namespace Course.BLL.Services
{
    public class AssignmentCompletionService : IAssignmentCompletionService
    {
        private readonly IAssignmentCompletionRepository _assignmentCompletionRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssignmentCompletionService(IAssignmentCompletionRepository assignmentCompletionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IAssignmentRepository assignmentRepository)
        {
            _assignmentCompletionRepository = assignmentCompletionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<Response<AssignmentCompletionDTO>> Add(Guid userId, AssignmentCompletionRequest assignmentCompletionRequest)
        {
            if (!await _assignmentRepository.FindByCondition(l => l.Id == assignmentCompletionRequest.AssignmentId).AnyAsync())
                return new Response<AssignmentCompletionDTO>(false, $"Not found assignment with id:{assignmentCompletionRequest.AssignmentId}", "404");

            if (await _assignmentCompletionRepository.BuildQuery()
                                                     .FilterByAssignment(assignmentCompletionRequest.AssignmentId)
                                                     .FilterByUser(userId)
                                                     .AnyAsync())
                return new Response<AssignmentCompletionDTO>(false, $"Duplicate assignment completion with assignment id:{assignmentCompletionRequest.AssignmentId}", "422");

            var assignmentCompletion = _mapper.Map<AssignmentCompletion>(assignmentCompletionRequest);

            assignmentCompletion.UserId = userId;

            await _assignmentCompletionRepository.CreateAsync(assignmentCompletion);
            await _unitOfWork.SaveChangesAsync();

            return new Response<AssignmentCompletionDTO>(true, _mapper.Map<AssignmentCompletionDTO>(assignmentCompletion));
        }


        public async Task<BaseResponse> IsCompletion(Guid userId, Guid assignmentId)
        {
            try
            {
                AssignmentCompletionRequest assignmentCompletionRequest = new AssignmentCompletionRequest();
                assignmentCompletionRequest.AssignmentId = assignmentId;

                var assignmentCompletion = _mapper.Map<AssignmentCompletion>(assignmentCompletionRequest);
                assignmentCompletion.UserId = userId;

                var result = await _assignmentCompletionRepository.IsCompletion(assignmentCompletion);
                return new BaseResponse(result);
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
