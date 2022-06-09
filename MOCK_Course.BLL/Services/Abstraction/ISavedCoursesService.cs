using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Share.RequestFeatures;

namespace Course.BLL.Services.Abstraction
{
    public interface ISavedCoursesService
    {
        Task<PagedList<SavedCoursesDTO>> GetAll(Guid userId, SavedCoursesParameters savedCoursesParameters);
        Task<Response<SavedCoursesDTO>> Add(Guid userId, Guid courseId);
        Task<BaseResponse> Remove(Guid userId);

    }
}
