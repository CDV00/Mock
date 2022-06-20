using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public AssignmentRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IAssignmentQuery BuildQuery()
        {
            return new AssignmentQuery(_context.Assignments.AsQueryable(), _context);
        }
        public async Task<PagedList<AssignmentDTO>> GetAllAssignment(AssignmentParameters parameter)
        {
            var assignment = await BuildQuery().IncludeSection()
                                               .IncludeAttachment()
                                              .FilterByKeyword(parameter.Keyword)
                                              .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                                              .Take(parameter.PageSize)
                                              .ToListAsync(d => _mapper.Map<AssignmentDTO>(d));

            var count = await BuildQuery().FilterByKeyword(parameter.Keyword)
                                                   .CountAsync();

            return new PagedList<AssignmentDTO>(assignment, count, parameter.PageNumber, parameter.PageSize);
        }

    }
}
