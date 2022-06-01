using Course.BLL.DTO;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IUploadService
    {
        Task<Response<UploadImageDTO>> UploadImage(IFormFile imageFile);
        Task<Response<UploadVideoDTO>> UploadVideo(IFormFile file);
    }
}