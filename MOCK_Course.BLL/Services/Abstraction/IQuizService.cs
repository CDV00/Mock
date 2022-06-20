using System.Threading.Tasks;
using Course.BLL.DTO;
using System;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;
using Entities.ParameterRequest;

namespace Course.BLL.Services.Abstraction
{
    public interface IQuizService
    {
        //Task<PagedList<QuizDTO>> GetAll(QuizParameters quizParameters);
        Task<ApiBaseResponse> GetAllQuiz(QuizParameters parameter);
    }
}
