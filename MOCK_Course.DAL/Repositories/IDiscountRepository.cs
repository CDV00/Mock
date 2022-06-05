using Course.DAL.Models;
using Course.DAL.Repositories;
using Course.Queries;
using System;

namespace Coursess.DAL.Repositories.Abstraction
{
    public interface IDiscountRepository : IRepository<Discount, Guid>
    {
        IDiscountQuery BuildQuery();
    }
}
