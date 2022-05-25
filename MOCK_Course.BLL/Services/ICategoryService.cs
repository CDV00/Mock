using System.Threading.Tasks;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using System;

namespace Course.BLL.Services
{
    public interface ICategoryService
    {
        Task<Responses<CategoryResponse>> GetAll();
        Task<Response<CategoryResponse>> Add(CategoryRequest categoryRequest);
        Task<BaseResponse> remove(Guid id);
        Task<Response<CategoryResponse>> Update(Guid id,CategoryUpdateRequest categoryUpdateRequest);
    }
}
