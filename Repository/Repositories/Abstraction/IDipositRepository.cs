using Course.DAL.Repositories.Abstraction;
using Entities.Models;
using Query.Abstraction;

namespace Repository.Repositories.Abstraction
{
    public interface IDipositRepository : IRepository<Deposit>
    {
        IDepositQuery BuildQuery();
    }
}