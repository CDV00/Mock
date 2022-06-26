using Course.BLL.DTO;
using Course.BLL.Requests;
using Entities.DTOs;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services.Abstraction
{
    public interface IQuizCompletionService
    {
        Task<Response<QuizCompletionDTO>> Add(Guid userId, QuizCompletionRequest lectureCompletionRequest);
        Task<BaseResponse> IsCompletion(Guid userId, Guid lectureId);
    }
}