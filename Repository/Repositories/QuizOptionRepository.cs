using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using System;

namespace Repository.Repositories
{
    public class QuizOptionRepository : Repository<QuizOption>, IQuizOptionRepository
    {
        private AppDbContext _context;
        public QuizOptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQuizOptionQuery BuildQuery()
        {
            return new QuizOptionQuery(_context.QuizOptions.AsQueryable(), _context);
        }

    }
}
