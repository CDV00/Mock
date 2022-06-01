using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.DAL.DTOs;

namespace Course.BLL.Services
{
    public interface ILevelService
    {
        Task<Responses<LevelDTO>> GetAll();
        //Task<Response<CategoryResponse>> Add(CategoryRequest categoryRequest);
        //Task<BaseResponse> remove(Guid id);
        //Task<Response<CategoryResponse>> Update(Guid id, CategoryUpdateRequest categoryUpdateRequest);
    }
}
