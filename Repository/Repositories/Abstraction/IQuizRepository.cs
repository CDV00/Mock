using Course.BLL.Requests;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        IQuizQuery BuildQuery();
        Task<PagedList<QuizDTO>> GetAllQuiz(QuizParameters parameter);
    }
}
