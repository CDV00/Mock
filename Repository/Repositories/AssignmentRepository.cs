using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using System;

namespace Repository.Repositories
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        private AppDbContext _context;
        public AssignmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IAssignmentQuery BuildQuery()
        {
            return new AssignmentQuery(_context.Assignments.AsQueryable(), _context);
        }

    }
}
