using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        IDiscountQuery BuildQuery();
        Task<bool> CheckDiscount(Discount discount);
    }
}
