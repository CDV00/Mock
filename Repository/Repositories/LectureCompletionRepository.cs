using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class LectureCompletionRepository : Repository<LectureCompletion>, ILectureCompletionRepository
    {
        private AppDbContext _context;
        public LectureCompletionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public ILectureCompletionQuery BuildQuery() => new LectureCompletionQuery(_context.LectureCompletions.AsQueryable(), _context);

        public async Task<bool> IsCompletion(LectureCompletion lessonCompletion)
        {
            if (await FindByCondition(l => l.UserId == lessonCompletion.UserId && l.LectureId == lessonCompletion.LectureId).FirstOrDefaultAsync() == null)
            {
                return false;
            }
            return true;
        }
    }
}
