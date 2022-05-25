using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;

namespace Course.BLL.Services
{
    public interface ICourseCompletionService
    {
        //Task<Responses<CourseCompletionResponse>> GetAll(Guid userId);
        Task<BaseResponse> IsCompletion(CourseCompletionRequest courseCompletionRequest); 
        Task<BaseResponse> Add(CourseCompletionRequest courseCompletionRequest);
        //Task<BaseResponse> Remove(Guid userId);
        //Task<Response<CourseCompletionResponse>> Update(CourseCompletionUpdateRequest courseCompletionUpdateRequest);
    }
}
