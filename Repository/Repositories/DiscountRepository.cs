using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Course.Queries;
using Entities.ParameterRequest;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public DiscountRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IDiscountQuery BuildQuery()
        {
            return new DiscountQuery(_context.Discounts.AsQueryable(), _context);
        }

        public async Task<PagedList<DiscountDTO_>> GetAllDiscount(Guid userId, DiscountParameters parameter)
        {
            var discounts = await BuildQuery().IncludeCourses()
                                              .FilterByUserId(userId)
                                              .ApplySort(parameter.OrderBy)
                                              .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                                              .Take(parameter.PageSize)
                                              .ToListAsync(d => _mapper.Map<DiscountDTO_>(d));

            var count = await BuildQuery().IncludeCourses()
                                          .FilterByUserId(userId)
                                          .CountAsync();

            return new PagedList<DiscountDTO_>(discounts, count, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<Discount> GetByIdAsync(Guid courseId, Guid Id)
        {
            var discount = await BuildQuery().FilterByCourseId(courseId)
                                             .FilterById(Id)
                                             .AsSelectorAsync(d => d);
            if (discount == null)
                return null;

            _context.Entry(discount).State = EntityState.Modified;

            return discount;
        }

        public async Task<bool> CheckDiscountTimeExisting(Guid courseId, DateTime startDate, DateTime endDate, Guid? id)
        {
            return await BuildQuery().FilterByCourseId(courseId)
                                     .FilterIgnoreId(id)
                                     .CheckDateDiscountExist(startDate, endDate)
                                     .CountAsync() > 0;
        }

    }
}
