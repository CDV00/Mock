using System.Threading.Tasks;
using Course.BLL.Requests;
using System;
using Course.DAL.Models;
using Entities.Responses;
using Entities.ParameterRequest;

namespace Course.BLL.Services.Abstraction
{
    public interface IDiscountService
    {
        Task<ApiBaseResponse> Add(DiscountForCreateRequest discountForCreateRequest, Guid userId);
        Task<ApiBaseResponse> Update(Guid id, DiscountForUpdateRequest discountForUpdateRequest, Guid userId);
        Task<ApiBaseResponse> Remove(Guid id, Guid userId);
        Task<ApiBaseResponse> GetAllDiscount(Guid userId, DiscountParameters parameter);
    }
}
