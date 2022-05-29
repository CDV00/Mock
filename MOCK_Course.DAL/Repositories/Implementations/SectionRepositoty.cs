using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class SectionRepositoty : Repository<Section, Guid>, ISectionRepositoty
    {
        public SectionRepositoty(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Section>> GetAllByCourseId(Guid courseId)
        {
            return await GetAll().Where(s => s.CourseId == courseId).ToListAsync();
        }

        public async Task<bool> RemoveByCourseId(Guid courseId)
        {
            var sections = await GetAll().Where(s => s.CourseId == courseId).ToListAsync();

            for (var i = 0; i < sections.Count; i++)
            {
                Remove(sections[i]);
            }

            return true;
        }
    }
}

