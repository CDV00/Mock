using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface IDiscountService
    {
        Task<Response<DiscountDTO_>> Add(DiscountForCreateRequest discountForCreateRequest);
        Task<Response<DiscountDTO_>> Update(Guid discountId, DiscountForUpdateRequest discountForUpdateRequest);
        Task<BaseResponse> Remove(Guid id);
        Task<Responses<DiscountDTO_>> GetAllDiscount(Guid userId);
    }
}
