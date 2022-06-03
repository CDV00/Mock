using Course.DAL.DTOs;
using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface IDiscountRepository : IRepository<Discount, Guid>
    {
        Task<List<DiscountDTO>> GetAllDiscount();
    }
}
