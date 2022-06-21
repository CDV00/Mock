using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Course.BLL.DTO;
using Dropbox.Api;
using Dropbox.Api.Files;
using Google.Apis.Download;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MOCK_Course.BLL.Services.Implementations;
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
        private readonly IGoogleService _googleService;
        public UploadFileService(IConfiguration configuration, IGoogleService googleService)
        {
            _configuration = configuration;
            _googleService = googleService;
        }

        public async Task<DownloadResponse> DownloadFile(string fileName)
        {

            try
            {
                var dbx = new DropboxClient(_configuration["DropboxServer:accessToken"]);
                using (var response = await dbx.Files.DownloadAsync(_configuration["DropboxServer:folder"] + fileName))
                {
                    var result = await response.GetContentAsByteArrayAsync();
                    Console.WriteLine(response.Response.IsDownloadable.ToString());
                    return new DownloadResponse()
                    {
                        IsSuccess = true,
                        fileData = result,
                        StatusCode = "Download success"
                    };
                };

            }
            catch (Exception ex)
            {
                return new DownloadResponse()
                {
                    IsSuccess = false,
                    StatusCode = ex.Message
                };
            }
        }
        public async Task<UploadResponse> UploadGoogleDrive(IFormFile file)
        {
            try
            {
                var serviceGoogle = await _googleService.CreateDriveService();

                var fileList = serviceGoogle.Files.List();


                var driveFile = new Google.Apis.Drive.v3.Data.File
                {
                    Name = file.FileName,
                    Parents = new String[]
                {
                    "15S5mwYkKEt7q8oaP_JIOxcorsAuTRZln"
                }
                };
                var request = serviceGoogle.Files.Create(driveFile, file.OpenReadStream(), "application/zip");
                request.Fields = "id";
                var response = await request.UploadAsync();
                if (response.Status == UploadStatus.Failed)
                {
                    return new UploadResponse()
                    {
                        IsSuccess = false,
                        Message = "Upload failed"
                    };
                }

                var fileRequest = request.ResponseBody;

                return new UploadResponse()
                {
                    IsSuccess = true,
                    url = fileRequest.Id
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
                    StatusCode = ex.Message
                };
            }
        }
        public async Task<DownloadResponse> DownloadFileGoogleDrive(string fileId)
        {
            try
            {
                var service = await _googleService.CreateDriveService();

                var request = service.Files.Get(fileId);

                var file = await request.ExecuteAsync();
                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {

                            case DownloadStatus.Failed:
                                {
                                    throw new Exception("Download Failed");
                                }
                        }
                    };
                var stream = new MemoryStream();
                await request.DownloadAsync(stream);
                var fileName = file.Name;
                var FileData = stream.ToArray();
                return new DownloadResponse()
                {
                    IsSuccess = true,
                    fileData = FileData,
                    Name = fileName,
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


        //public async Task<UploadResponse> UploadGoogleDrive(IFormFile file)
        //{
        //    try
        //    {
        //        var serviceGoogle = await _googleService.CreateDriveService();

        //        var fileList = serviceGoogle.Files.List();


        //        var driveFile = new Google.Apis.Drive.v3.Data.File
        //        {
        //            Name = file.FileName,
        //            Parents = new String[]
        //        {
        //            "15S5mwYkKEt7q8oaP_JIOxcorsAuTRZln"
        //        }
        //        };
        //        var request = serviceGoogle.Files.Create(driveFile, file.OpenReadStream(), "application/zip");
        //        request.Fields = "id";
        //        var response = await request.UploadAsync();
        //        if (response.Status == UploadStatus.Failed)
        //        {
        //            return new UploadResponse()
        //            {
        //                IsSuccess = false,
        //                Message = "Upload failed"
        //            };
        //        }

        //        var fileRequest = request.ResponseBody;

        //        return new UploadResponse()
        //        {
        //            IsSuccess = true,
        //            url = fileRequest.Id
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new UploadResponse()
        //        {
        //            IsSuccess = false,
        //            StatusCode = ex.Message
        //        };
        //    }

        //}

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
                        StatusCode = "Image null"
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
                    StatusCode = ex.Message.ToString()
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
                    StatusCode = "file null"
                };

            }
            catch (Exception ex)
            {
                return new UploadResponse()
                {
                    IsSuccess = false,
                    StatusCode = ex.Message
                };
            }
        }





    }
}
