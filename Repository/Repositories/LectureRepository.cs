using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class LectureRepository : Repository<Lecture>, ILectureRepository
    {
        private AppDbContext _context;
        public LectureRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public ILectureQuery BuildQuery() => new LectureQuery(_context.Lectures.AsQueryable(), _context);

        public async Task<bool> RemoveBySectionId(Guid sectionId)
        {
            var lessons = await GetAll().Where(s => s.SectionId == sectionId).ToListAsync();

            for (var i = 0; i < lessons.Count; i++)
            {
                Remove(lessons[i], false);
            }

            return true;
        }
    }
}
