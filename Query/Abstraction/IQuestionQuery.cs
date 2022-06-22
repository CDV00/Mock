using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface IQuestionQuery : IQuery<Question>
    {
        public IQuestionQuery FilterByQuizId(Guid quizId);
    }
}