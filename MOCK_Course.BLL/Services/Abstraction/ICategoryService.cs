using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface ICategoryService
    {
        Task<Responses<CategoryDTO_>> GetAll();
        Task<Response<CategoryDTO_>> Add(CategoryRequest categoryRequest);
        Task<BaseResponse> remove(Guid id);
        Task<Response<CategoryDTO_>> Update(Guid id, CategoryUpdateRequest categoryUpdateRequest);
    }
}
