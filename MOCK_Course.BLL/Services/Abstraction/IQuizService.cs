using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;

namespace Course.BLL.Services.Abstraction
{
    public interface IQuizService
    {
        Task<PagedList<QuizDTO>> GetAll(QuizParameters quizParameters);
    }
}
