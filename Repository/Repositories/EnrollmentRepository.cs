using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment, Guid>, IEnrollmentRepository
    {
        private AppDbContext _context;
        public EnrollmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnrollmentQuery BuildQuery()
        {
            return new EnrollmentQuery(_context.Enrollment.AsQueryable(), _context);
        }

        //public async Task<bool> IsEnrollmented(Enrollment enrollment)
        //{
        //    if (await FindByCondition(l => l.UserId == enrollment.UserId && l.CourseId == enrollment.CourseId).FirstOrDefaultAsync() == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //public async Task<int> GetTotal(Guid userId)
        //{
        //    return await GetAll().Where(s => s.UserId == userId).GroupBy(s => s.UserId).CountAsync();
        //}
    }
}
