using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class LectureCompletionRepository : Repository<LectureCompletion>, ILectureCompletionRepository
    {
        public LectureCompletionRepository(AppDbContext context) : base(context)
        {

        }

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
