using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        IDiscountQuery BuildQuery();
        Task<Discount> GetByIdAsync(Guid courseId, Guid Id);
    }
}
