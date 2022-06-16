using Course.BLL.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IUploadFileService
    {
        public Task<UploadResponse> UploadVideo(IFormFile file);
        public Task<UploadResponse> UploadImage(IFormFile file);
        public Task<UploadResponse> UploadFile(IFormFile file);
        public Task<DownloadResponse> DownloadFile(string fileName);
       public Task<UploadResponse> UploadGoogleDrive(IFormFile file);
    }
}
