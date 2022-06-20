using System.Threading.Tasks;
using Course.BLL.DTO;
using System;
using Course.BLL.Share.RequestFeatures;
using Entities.Responses;
using Entities.ParameterRequest;

namespace Course.BLL.Services.Abstraction
{
    public interface ISavedCoursesService
    {
        Task<ApiBaseResponse> Add(Guid userId, Guid courseId);
        Task<ApiBaseResponse> RemoveAll(Guid userId);
        Task<ApiOkResponse<bool>> IsSaveCourses(Guid userId, Guid courseId);

        Task<ApiBaseResponse> GetAll(Guid userId, SavedCoursesParameters parameters);
        Task<ApiBaseResponse> Remove(Guid courseId, Guid userId);
    }
}
