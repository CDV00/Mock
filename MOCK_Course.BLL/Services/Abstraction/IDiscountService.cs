using System.Threading.Tasks;
using Course.BLL.Requests;
using System;
using Course.DAL.Models;
using Entities.Responses;
using Entities.Requests;

namespace Course.BLL.Services.Abstraction
{
    public interface IDiscountService
    {
        Task<ApiBaseResponse> Add(DiscountForCreateRequest discountForCreateRequest);
        Task<ApiBaseResponse> Update(Guid id, DiscountForUpdateRequest discountForUpdateRequest);
        Task<ApiBaseResponse> Remove(Guid id);
        Task<ApiBaseResponse> GetAllDiscount(Guid userId, DiscountParameters parameter);
    }
}
