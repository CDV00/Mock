using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class LectureRepository : Repository<Lecture, Guid>, ILectureRepository
    {
        public LectureRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Lecture>> GetAllBySectionId(Guid sectionId)
        {
            return await GetAll().Where(s => s.SectionId == sectionId).ToListAsync();
        }

        public async Task<bool> RemoveBySectionId(Guid sectionId)
        {
            var lessons = await GetAll().Where(s => s.SectionId == sectionId).ToListAsync();

            for (var i = 0; i < lessons.Count; i++)
            {
                Remove(lessons[i]);
            }

            return true;
        }
    }
}
