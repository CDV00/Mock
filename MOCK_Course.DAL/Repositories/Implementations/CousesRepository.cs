using Course.DAL.Data;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class CousesRepository : Repository<Courses, Guid>, ICousesRepository
    {
        private AppDbContext _context;
        public CousesRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override Task CreateAsync(Courses _object)
        {

            return base.CreateAsync(_object);
        }

        public async Task<IEnumerable<Courses>> GetAllForCard()
        {

            return await GetAll().Include(c => c.Category).Include(c => c.User).ToListAsync();
        }

        public async Task<Courses> GetForPost(Guid id)
        {
            return await FindByCondition(c => c.Id == id).Include(c => c.AudioLanguages).Include(c => c.CloseCaptions).Include(c => c.Sections).ThenInclude(s => s.Lecture).Include(c => c.CourseLevels).FirstOrDefaultAsync();
        }
        public async Task<int> GetTotal(Guid userId)
        {
            return await GetAll().Where(s => s.UserId == userId).GroupBy(s => s.UserId).CountAsync();
        }

        public async Task<List<PurchaseDTO>> GetAllMyPurchase(Guid userId)
        {
            var purchases = await (from order in _context.Orders
                                   join course in _context.Courses on order.CourseId equals course.Id
                                   join user in _context.Users on course.UserId equals user.Id
                                   join category in _context.Categories on course.CategoryId equals category.Id
                                   where (order.UserId == userId)
                                   select new PurchaseDTO
                                   {
                                       FullName = user.Fullname,
                                       Title = course.Title,
                                       Category = category.Name,
                                       Price = order.Price,
                                       CreatedAt = order.CreatedAt
                                   }).ToListAsync();

            return purchases;
        }

    }
}
