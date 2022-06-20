using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Entities.ParameterRequest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        IDiscountQuery BuildQuery();
        Task<bool> CheckDiscountTimeExisting(Guid courseId, DateTime startDate, DateTime endDate, Guid? id);
        Task<PagedList<DiscountDTO_>> GetAllDiscount(Guid userId, DiscountParameters parameter);
        Task<Discount> GetByIdAsync(Guid courseId, Guid Id);
    }
}
