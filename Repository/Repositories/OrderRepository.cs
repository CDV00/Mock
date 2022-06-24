using AutoMapper;
using Course.BLL.Responses;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using System;
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
    }
}
