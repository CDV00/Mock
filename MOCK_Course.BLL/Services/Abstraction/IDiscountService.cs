using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface IDiscountService
    {
        Task<Response<DiscountDTO>> Add(DiscountForCreateRequest discountForCreateRequest);
        Task<Response<DiscountDTO>> Update(Guid discountId, DiscountForUpdateRequest discountForUpdateRequest);
        Task<BaseResponse> Remove(Guid id);
        Task<Responses<DiscountDTO>> GetAllDiscount(Guid userId);
    }
}
