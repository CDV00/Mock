using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public OrderRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IOrderQuery BuildQuery()
        {
            return new OrderQuery(_context.Orders.AsQueryable(), _context);
        }
        public async Task<OrderDTO> GetDetailOrder(Guid id)
        {
            var order = await BuildQuery().IncludeCourse()
                                           .IncludeUser()
                                           .IncludeDiscount()
                                           .FilterById(id)
                                           .AsSelectorAsync(x => _mapper.Map<OrderDTO>(x));

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<PagedList<EarningDTO>> GetEarningAsync(OrderParameters orderParameters, Guid userId)
        {
            var earning = await BuildQuery().FilterByUserIdInstructor(userId)
                                            .FilterStartDate(orderParameters.startDay)
                                            .FilterEndtDate(orderParameters.endDate)
                                            //.GroupByCreateAt()
                                            .Skip((orderParameters.PageNumber - 1) * orderParameters.PageSize)
                                            .Take(orderParameters.PageSize)
                                            .ApplySort(orderParameters.Orderby)
                                            .ToListAsync(o => _mapper.Map<EarningDTO>(o));


            var earningCount = await BuildQuery().FilterByUserIdInstructor(userId)
                                            .FilterStartDate(orderParameters.startDay)
                                            .FilterEndtDate(orderParameters.endDate)
                                            .ToListAsync(o => _mapper.Map<EarningDTO>(o));
            await AddLast(earning, userId);
            await AddLast(earningCount, userId);
            int count = earningCount.Count;
            return new PagedList<EarningDTO>(earning, count, orderParameters.PageNumber, orderParameters.PageSize);
        }
        //
        private async Task<int> couutEarning(List<EarningDTO> earning, Guid userId)
        {
            for (var i = 0; i < earning.Count; i++)
            {
                if (earning.Find(c => c.CreatedAt.Date == earning[i].CreatedAt.Date).Count > 1)
                {
                    earning.Remove(earning[i]);
                    i--;
                }
            }
            return  earning.Count;
        }
        //
        private async Task AddLast(List<EarningDTO> earning, Guid userId)
        {
            for (var i = 0; i < earning.Count; i++)
            {
                if (earning.Find(c => c.CreatedAt.Date == earning[i].CreatedAt.Date).Count > 1)
                {
                    earning.Remove(earning[i]);
                    i--;
                    continue;
                }
                earning[i].Earning = (await TotalPriceDate(userId, earning[i].CreatedAt));
                earning[i].Count = (await CountOrderDate(userId, earning[i].CreatedAt));
            }
        }
        //
        private async Task<decimal> TotalPriceDate(Guid userId, DateTime createAt)
        {
            decimal totalPrice = await BuildQuery().FilterByUserIdInstructor(userId).FilterByCreateAt(createAt).SumAsync(o => (long)o.TotalPrice);
            return totalPrice;
        }
        //
        private async Task<int> CountOrderDate(Guid userId, DateTime createAt)
        {
            int count = await BuildQuery().FilterByUserIdInstructor(userId).FilterByCreateAt(createAt).CountAsync();
            return count;
        }
    }
}
