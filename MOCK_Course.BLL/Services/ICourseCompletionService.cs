using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;

namespace Course.BLL.Services
{
    public interface ICourseCompletionService
    {
        Task<BaseResponse> IsCompletion(CourseCompletionRequest courseCompletionRequest); 
        Task<BaseResponse> Add(CourseCompletionRequest courseCompletionRequest);
    }
}
