using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Drive.v3.DriveService;

namespace Course.BLL.Services
{
    public class GoogleService
    {
        public GoogleService()
        {

        }

        public static DriveService CreateService()
        {
            try
            {
                var tokenResponse = new TokenResponse()
                {
                    AccessToken = "ya29.a0ARrdaM_78pBBAK3sFgHVOwy7KQzjrn0iuTjvp7aM8ZUYJAGBXt5etjzHXrKJz0CRVkSE4OW28SIJADas2Gxpt8nQe4ma0TGM8vMzUBX0nUTyhz0HlX0ajdyO5nqFeiMriZrqaJw4JLNm6wxmu50Uz5JkB_fo",
                    RefreshToken = "1//04vPH_fFouzGSCgYIARAAGAQSNwF-L9IrARsNDLgzu7UxfA3KKUwqISoYEZrwTuTEFmWL6_NGfpqfIGfN_C2PNzF9h_ch3c6IgN8"
                };
                var applicationName = "Django-141099";
                var userName = "kvlanh1410@gmail.com";
                var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = "344123500469-hu3reila4nqbltvg0eujsnf21tob99sq.apps.googleusercontent.com",
                        ClientSecret = "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"
                    },
                    Scopes = new[] {Scope.Drive },
                    DataStore = new FileDataStore(applicationName)
                });
                var credential = new UserCredential(apiCodeFlow, userName, tokenResponse);
                var service = new DriveService(new BaseClientService.Initializer
                 {
                     HttpClientInitializer = credential,
                     ApplicationName = applicationName
                 });
                return service;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
