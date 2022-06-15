using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using System;

namespace Repository.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private AppDbContext _context;
        public QuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQuestionQuery BuildQuery()
        {
            return new QuestionQuery(_context.Questions.AsQueryable(), _context);
        }

    }
}
