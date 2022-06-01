using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Course.BLL.DTO;
using Course.BLL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using VimeoDotNet;
using VimeoDotNet.Net;

namespace Course.BLL.Services.Implementations
{
    public class UploadService : IUploadService
    {
        private readonly IConfiguration _configuration;
        public UploadService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Response<UploadImageDTO>> UploadImage(IFormFile imageFile)
        {
            try
            {
                var account = new Account(
                 _configuration["ServerImage:server"],
                 _configuration["ServerImage:apiKey"],
                 _configuration["ServerImage:apiSecret"]
                 );

                if (imageFile is null || imageFile.Length <= 0)
                {
                    return new Response<UploadImageDTO>(false, "Image is null", null);
                }

                var cloudinary = new Cloudinary(account);

                using var stream = imageFile.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(imageFile.FileName, stream),
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                return new Response<UploadImageDTO>(true, new UploadImageDTO() { Url = uploadResult.Url.ToString() });
            }
            catch (Exception ex)
            {
                return new Response<UploadImageDTO>(false, ex.Message, null);
            }
        }


        public async Task<Response<UploadVideoDTO>> UploadVideo(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    var vimeoClient = new VimeoClient(_configuration["VideoServer:accessToken"]);
                    var authCheck = await vimeoClient.GetAccountInformationAsync();
                    if (authCheck.Name != null)
                    {
                        IUploadRequest uploadRequest = new UploadRequest();
                        var binaryContent = new BinaryContent(file.OpenReadStream(), file.ContentType);
                        int chunkSize = 0;
                        var contentLenght = (int)file.Length;
                        var temp = contentLenght / 1024;
                        if (temp > 1)
                        {
                            chunkSize = temp / 1024;
                            chunkSize = chunkSize * 1048576;
                        }
                        else
                        {
                            chunkSize = 1048576;
                        }

                        uploadRequest = await vimeoClient.UploadEntireFileAsync(binaryContent, chunkSize, null);

                        var duration = uploadRequest.Ticket.Video?.Duration;

                        var urlVideo = string.Concat("https://player.vimeo.com/video/", uploadRequest.ClipId.Value.ToString());

                        return new Response<UploadVideoDTO>(true, new UploadVideoDTO() { Url = urlVideo, Duration = duration });
                    }
                }
                return new Response<UploadVideoDTO>(false, "file null", null);

            }
            catch (Exception ex)
            {
                return new Response<UploadVideoDTO>(false, ex.Message, null);
            }
        }
    }
}
