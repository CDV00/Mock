using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using static Google.Apis.Drive.v3.DriveService;

namespace MOCK_Course.BLL.Services.Implementations
{
    public class GoogleService : IGoogleService
    {
        private readonly IConfiguration _configuration;

        public GoogleService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DriveService> CreateDriveService()
        {
            try
            {
                var tokenResponse = new TokenResponse()
                {
                    AccessToken = _configuration["GoogleService:AccessToken"],
                    RefreshToken = _configuration["GoogleService:RefreshToken"]
                };
                var applicationName = _configuration["GoogleServiceInfo:ApplicationName"];
                var userName = _configuration["GoogleServiceInfo:Username"];
                var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = _configuration["GoogleServiceInfo:CliendId"],
                        ClientSecret = _configuration["GoogleServiceInfo:ClientSecret"]
                    },
                    Scopes = new[] { Scope.Drive },
                    DataStore = new FileDataStore(applicationName)
                });
                var credential = new UserCredential(apiCodeFlow, userName, tokenResponse);
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName
                });
                return await Task.FromResult(service);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //public async Task<YouTubeService> CreateYouTubeService()
        //{
        //    try
        //    {
        //        var tokenResponse = new TokenResponse()
        //        {
        //            AccessToken = _configuration["GoogleService:AccessToken"],
        //            RefreshToken = _configuration["GoogleService:RefreshToken"]
        //        };

        //        var applicationName = _configuration["GoogleServiceInfo:ApplicationName"];
        //        var userName = _configuration["GoogleServiceInfo:Username"];
        //        var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        //        {
        //            ClientSecrets = new ClientSecrets
        //            {
        //                ClientId = _configuration["GoogleServiceInfo:CliendId"],
        //                ClientSecret = _configuration["GoogleServiceInfo:ClientSecret"]
        //            },
        //            Scopes = new[] { YouTubeService.Scope.YoutubeUpload },
        //            DataStore = new FileDataStore(applicationName)
        //        });
        //        var credential = new UserCredential(apiCodeFlow, userName, tokenResponse);

        //        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        //        {
        //            HttpClientInitializer = credential,
        //            ApplicationName = applicationName
        //        });
        //        return await Task.FromResult(youtubeService);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
