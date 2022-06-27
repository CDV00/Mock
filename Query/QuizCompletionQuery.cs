using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Query.Abstraction;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Query
{
    public class QuizCompletionQuery : QueryBase<QuizCompletion>, IQuizCompletionQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public QuizCompletionQuery(IQueryable<QuizCompletion> QuizCompletion, AppDbContext dbContext) : base(QuizCompletion)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }

        public QuizCompletionQuery FilterByUser(Guid userId)
        {
            Query = Query.Where(type => type.UserId == userId);
            return this;
        }
        public QuizCompletionQuery FilterByQuiz(Guid quizId)
        {
            Query = Query.Where(type => type.QuizId == quizId);
            return this;
        }
    }
}
