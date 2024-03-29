﻿using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using Microsoft.EntityFrameworkCore;
using Query.Abstraction;
using Repository.Repositories.Abstraction;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public OrderItemRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IOrderItemQuery BuildQuery()
        {
            return new OrderItemQuery(_context.OrderItems.AsQueryable(), _context);
        }

        public async Task<PagedList<OrderItemDTO>> GetStatements(DepositParameters depositParameters, Guid userid)
        {
            var statements = await BuildQuery().FilterByUserIdInstructor(userid)
                                               .FilterStartDate(depositParameters.startDate)
                                               .FilterEndtDate(depositParameters.endDate)
                                               .IncludeCourse()
                                               .ApplySort(depositParameters.Orderby)
                                               .Skip((depositParameters.PageNumber - 1) * depositParameters.PageSize)
                                               .Take(depositParameters.PageSize)
                                               .ToListAsync(d => _mapper.Map<OrderItemDTO>(d));

            var count = await BuildQuery().FilterByUserId(userid)
                                          .CountAsync();
            var pageList = new PagedList<OrderItemDTO>(statements, count, depositParameters.PageNumber, depositParameters.PageSize);
            return pageList;
        }

        public async Task<PagedList<EarningDTO>> GetEarningAsync(OrderParameters orderParameters, Guid userId)
        {
            //var result = await _context.Orders.GroupBy(o => o.CreatedAt.Date)
            //    .Select(o => new EarningDTO
            //    {
            //        Earning = o.Select(c => c.TotalPrice).Sum(),
            //        CreatedAt = o.Key,
            //        Count = o.Select(c => c).Count(),
            //    })
            //    .ToListAsync();

            //return result;
            return null;
        }
    }
}
