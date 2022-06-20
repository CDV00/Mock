using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public ShoppingCartRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IShoppingCartQuery BuildQuery()
        {
            return new ShoppingCartQuery(_context.Carts.AsQueryable(), _context);
        }
        public async Task<bool> CheckPriceGreaterThanZero(Guid courseId)
        {
            decimal price = await BuildQuery().FilterByCourseId(courseId)
                                              .IncludeCourse()
                                              .AsSelectorAsync(s => s.Course.Price);
            if (price <= 0)
            {
                return true;
            }
            return false;
        }

        public async Task<PagedList<CartDTO>> GetAllAsync(CartParameters parameters, Guid userId)
        {
            var Carts = await BuildQuery().FilterByUserId(userId)
                                          .IncludeCourse()
                                          .IncludeUser()
                                          .IncludeCategory()
                                          .IncludeDiscount()
                                          .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                          .Take(parameters.PageSize)
                                          .ToListAsync(c => _mapper.Map<CartDTO>(c));

            var count = await BuildQuery().FilterByUserId(userId)
                                          .CountAsync();

            return new PagedList<CartDTO>(Carts, count, parameters.PageNumber, parameters.PageSize);
        }
    }
}
