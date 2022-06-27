using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public UserRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IUserQuery BuildQuery()
        {
            return new UserQuery(_context.Users.AsQueryable(), _context);
        }
        public async Task<PagedList<UserDTO>> GetAllUserByRole(UserParameter parameter)
        {
            var users = await BuildQuery()
                            .FilterByRole(parameter.Role)
                            .FilterByKeyword(parameter.Keyword)
                            .ApplySort(parameter.Orderby)
                            .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                            .Take(parameter.PageSize)
                            .ToListAsync(c => _mapper.Map<UserDTO>(c));
            foreach (var user in users)
            {
                user.Role = parameter.Role;
            }

            var count = await BuildQuery().FilterByRole(parameter.Role)
                                          .FilterByKeyword(parameter.Keyword)
                                          .CountAsync();

            return new PagedList<UserDTO>(users, count, parameter.PageNumber, parameter.PageSize);
        }



    }
}
