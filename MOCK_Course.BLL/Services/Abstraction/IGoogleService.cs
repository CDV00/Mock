using Google.Apis.Drive.v3;
using System.Threading.Tasks;

namespace Course.BLL.Services.Abstraction
{
    public interface IGoogleService
    {
        Task<DriveService> CreateDriveService();
        //Task<YouTubeService> CreateYouTubeService();
    }
}