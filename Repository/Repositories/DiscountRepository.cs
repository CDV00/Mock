using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Course.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private AppDbContext _context;
        public DiscountRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IDiscountQuery BuildQuery()
        {
            return new DiscountQuery(_context.Discounts.AsQueryable(), _context);
        }

        public async Task<bool> CheckDiscount(Discount discount)
        {
            int discounts = await BuildQuery().FilterByCourseId(discount.CourseId)
                                              .checkDate(discount)
                                              .CountAsync();
            if (discounts>0)
            {
                return true;
            }
            return false;
        }
        //public async Task<List<DiscountDTO>> GetAllDiscount()
        //{
        //    var discount = await (from discounts in _context.Discounts
        //                          join course in _context.Courses on discounts.CourseId equals course.Id
        //                          select new DiscountDTO
        //                          {
        //                              Id = discounts.Id,
        //                              CourseName = course.Title,
        //                              StartDate = discounts.StartDate,
        //                              EndDate = discounts.EndDate,
        //                              DiscountPercent = discounts.DiscountPercent,
        //                              Status = discounts.IsActive

        //                          }).ToListAsync();
        //    return discount;
        //}
        /*public async Task<List<DiscountDTO>> GetAllDiscount();
        {
           var discount = await(from discount in _context.Discounts
                                join course in _context.Courses on order.CourseId equals course.Id
                                where (order.UserId == userId)
                                select new PurchaseDTO
                                {
                                    FullName = user.Fullname,
                                    Title = course.Title,
                                    Category = category.Name,
                                    Price = order.Price,
                                    CreatedAt = order.CreatedAt
                                }).ToListAsync();

           return discount;
        }*/
}
}
