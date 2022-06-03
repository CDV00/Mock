using Course.BLL.Responses;
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
    public class DiscountRepository : Repository<Discount, Guid>, IDiscountRepository
    {
        private AppDbContext _context;
        public DiscountRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DiscountDTO>> GetAllDiscount()
        {
            var discount = await (from discounts in _context.Discounts
                                  join course in _context.Courses on discounts.CourseId equals course.Id
                                  select new DiscountDTO
                                  {
                                      Id = discounts.Id,
                                      CourseName = course.Title,
                                      StartDate = discounts.StartDate,
                                      EndDate = discounts.EndDate,
                                      DiscountPercent = discounts.DiscountPercent,
                                      Status = discounts.IsActive

                                  }).ToListAsync();
            return discount;
        }
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
