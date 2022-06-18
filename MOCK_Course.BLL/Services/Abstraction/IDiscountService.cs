using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;
using Course.DAL.Models;

namespace Course.BLL.Services.Abstraction
{
    public interface IDiscountService
    {
        Task<Response<DiscountDTO_>> Add(DiscountForCreateRequest discountForCreateRequest, Courses course);
        Task<Response<DiscountDTO_>> Update(Discount discount, DiscountForUpdateRequest discountForUpdateRequest);
        Task<BaseResponse> Remove(Discount discount);
        Task<Responses<DiscountDTO_>> GetAllDiscount(Guid userId);
    }
}
