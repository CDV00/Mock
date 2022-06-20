using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Course.BLL.DTO;
using Dropbox.Api;
using Dropbox.Api.Files;
using Google.Apis.Download;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VimeoDotNet;
using VimeoDotNet.Net;

namespace Course.BLL.Services
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IConfiguration _configuration;
        public UploadFileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DownloadResponse> DownloadFile(string fileName)
        {

            try
            {
                var dbx = new DropboxClient(_configuration["DropboxServer:accessToken"]);
                using (var response = await dbx.Files.DownloadAsync(_configuration["DropboxServer:folder"] + fileName))
                {
                    var result =  await response.GetContentAsByteArrayAsync();
                    Console.WriteLine(response.Response.IsDownloadable.ToString());
                    return new DownloadResponse()
                    {
                        IsSuccess = true,
                        fileData = result,
                        Message = "Download success"
                    };
                };

            }
            catch (Exception ex)
            {
                return new DownloadResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<DownloadResponse> DownloadFileGoogleDrive(string fileId)
        {
            try
            {
                var service = GoogleService.CreateService();
                var response = new DownloadResponse()
                {
                    IsSuccess = true
                };
                var request = service.Files.Export(fileId, "application/pdf");
                var stream = new MemoryStream();

                // Add a handler which will be notified on progress changes.
                // It will notify on each chunk download and when the
                // download is completed or failed.

                //request.MediaDownloader.ProgressChanged +=
                //    progress =>
                //    {
                //        switch (progress.Status)
                //        {

                //            case DownloadStatus.Failed:
                //                {
                //                    response.IsSuccess = false;
                //                    break;
                //                }

                //        }
                //    };
                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("Download complete.");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("Download failed.");
                                    break;
                                }
                        }
                    };

                request.Download(stream);
                if (!response.IsSuccess)
                {
                    response.Message = "Upload failed";
                }
                else
                {
                    response.Message = "Upload Success";
                    response.fileData = stream.ToArray();
                }
                return response;

            }
            catch (Exception ex)
            {
                return new DownloadResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<UploadResponse> UploadFile(IFormFile file)
        {
            try
            {
                var dbx = new DropboxClient(_configuration["DropboxServer:AppKey"]);
                var filePath = _configuration["DropboxServer:folder"] + Guid.NewGuid() + file.FileName;
                var uploadResult = await dbx.Files.UploadAsync(filePath, WriteMode.Add.Instance, body: file.OpenReadStream());
                return new UploadResponse()
                {
                    IsSuccess = true,
                    url = uploadResult.Name
                };
            }
            catch (Exception ex)
            {
                return new UploadResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UploadResponse> UploadGoogleDrive(IFormFile file)
        {
            try
            {
                var serviceGoogle = GoogleService.CreateService();

                var fileList = serviceGoogle.Files.List();


                var driveFile = new Google.Apis.Drive.v3.Data.File();
                driveFile.Name = file.FileName;
                driveFile.Parents = new String[]
                {
                    "1VTJ8KaO4cL6fYi-8I_-6k9G2U7ByGcFT"
                };
                var request = serviceGoogle.Files.Create(driveFile, file.OpenReadStream(), null);
                request.Fields = "id";
                var response = await request.UploadAsync();
                return new UploadResponse()
                {
                    IsSuccess = false,
                    url = response.Status.ToString()

                };
            }
            catch (Exception ex)
            {
                return new UploadResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<UploadResponse> UploadImage(IFormFile imageFile)
        {
            try
            {
                var account = new Account(
                 _configuration["ServerImage:server"],
                 _configuration["ServerImage:apiKey"],
                 _configuration["ServerImage:apiSecret"]
                 );

                if (imageFile is null)
                {
                    return new UploadResponse()
                    {
                        IsSuccess = false,
                        Message = "Image null"
                    };
                }
                var cloudinary = new Cloudinary(account);
                if (imageFile.Length > 0)
                {
                    using var stream = imageFile.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(imageFile.FileName, stream),
                    };
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    return new UploadResponse()
                    {
                        IsSuccess = true,
                        url = uploadResult.Url.ToString()
                    };
                }
                return new UploadResponse()
                {
                    IsSuccess = true,
                    url = "",
                };
            }
            catch (Exception ex)
            {
                return new UploadResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message.ToString()
                };
            }
        }

        public async Task<UploadResponse> UploadVideo(IFormFile file)
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
                        var urlVideo = string.Concat("https://player.vimeo.com/video/", uploadRequest.ClipId.Value.ToString());
                        return new UploadResponse()
                        {
                            IsSuccess = true,
                            url = urlVideo
                        };
                    }


                }
                return new UploadResponse()
                {
                    IsSuccess = false,
                    Message = "file null"
                };

            }
            catch (Exception ex)
            {
                return new UploadResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }





    }
}
