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
    public class SectionRepositoty : Repository<Section, Guid>, ISectionRepositoty
    {
        private AppDbContext _context;
        public SectionRepositoty(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public ISectionQuery BuildQuery()
        {
            return new SectionQuery(_context.Sections.AsQueryable(), _context);
        }

        //public async Task<IEnumerable<Section>> GetAllByCourseId(Guid courseId)
        //{
        //    return await GetAll().Where(s => s.CourseId == courseId).ToListAsync();
        //}

        //public async Task<bool> RemoveByCourseId(Guid courseId)
        //{
        //    var sections = await GetAll().Where(s => s.CourseId == courseId).ToListAsync();

        //    for (var i = 0; i < sections.Count; i++)
        //    {
        //        Remove(sections[i]);
        //    }

        //    return true;
        //}
    }
}

