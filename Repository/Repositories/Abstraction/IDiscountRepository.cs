using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IDiscountRepository : IRepository<Discount, Guid>
    {
        IDiscountQuery BuildQuery();
    }
}
