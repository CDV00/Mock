using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Repositories.Implementations;
using Course.Queries;
using Coursess.DAL.Repositories.Abstraction;
using System;
using System.Linq;

namespace Coursess.DAL.Repositories.Implementations
{
    public class DiscountRepository : Repository<Discount, Guid>, IDiscountRepository
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
        } */
    }
}
