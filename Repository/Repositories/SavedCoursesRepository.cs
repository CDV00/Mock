using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;

namespace Repository.Repositories
{
    public class SavedCoursesRepository : Repository<SavedCourses>, ISavedCoursesRepository
    {
        private AppDbContext _context;
        public SavedCoursesRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public ISavedCoursesQuery BuildQuery()
        {
            return new SavedCoursesQuery(_context.SavedCourses.AsQueryable(), _context);
        }
    }
}
