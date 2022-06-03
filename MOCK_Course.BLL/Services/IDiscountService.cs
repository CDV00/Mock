using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;
using Course.DAL.DTOs;

namespace Course.BLL.Services
{
    public interface IDiscountService
    {
        Task<Responses<DiscountDTO>> GetAllDiscount();
        Task<Response<DiscountForCreateDTO>> Add(DiscountForCreateRequest discountForCreateRequest);
        Task<Response<DiscountForUpdateDTO>> Update(Guid discountId, DiscountForUpdateRequest discountForUpdateRequest);
        Task<BaseResponse> Remove(Guid id);
    }
}
