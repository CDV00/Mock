using Course.DAL.Data;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Entities.Models;

namespace Repository.Repositories
{
    public class DipositRepository : Repository<Deposit>, IDipositRepository
    {
        private AppDbContext _context;
        public DipositRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IDepositQuery BuildQuery()
        {
            return new DepositQuery(_context.Deposits.AsQueryable(), _context);
        }
    }
}
