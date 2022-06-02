using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;

namespace Course.BLL.Services
{
    public interface ICategoryService
    {
        Task<Responses<CategoryDTO>> GetAll();
        Task<Response<CategoryDTO>> Add(CategoryRequest categoryRequest);
        Task<BaseResponse> remove(Guid id);
        Task<Response<CategoryDTO>> Update(Guid id, CategoryUpdateRequest categoryUpdateRequest);
    }
}
