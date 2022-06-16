using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.BLL.DTO;

namespace Course.BLL.Services
{
    public interface IGoogleDriveService 
    {
        public Task<BaseResponse> UploadFile(IFormFile file);

    }
}
