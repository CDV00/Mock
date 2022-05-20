using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.BLL.Requests;

namespace Course.BLL.Services
{
    public interface ICategoryService
    {
        Task<Responses<CategoryResponse>> GetAll();
        Task<Response<CategoryResponse>> Add(CategoryRequest categoryRequest);
    }
}
