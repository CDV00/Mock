using Google.Apis.Drive.v3;
using System.Threading.Tasks;

namespace MOCK_Course.BLL.Services.Implementations
{
    public interface IGoogleService
    {
        Task<DriveService> CreateDriveService();
        //Task<YouTubeService> CreateYouTubeService();
    }
}