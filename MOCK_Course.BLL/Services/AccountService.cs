using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.ConfigurationModels;
using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Google.Apis.Auth;
using System.Net.Http;
using Newtonsoft.Json;
using static Course.BLL.Responses.FacebookApiDTO;
using Course.BLL.Services.Abstraction;
using Entities.Constants;

namespace Course.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;
        //private readonly IConfiguration _configuration;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IOptions<JwtConfiguration> _configuration;
        private readonly IConfiguration _configurations;
        private AppUser _user;
        private UserDTO _userResponse = new UserDTO();
        private static readonly HttpClient Client = new();
        public AuthenticationService(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<Guid>> roleManager, ILogger<AuthenticationService> logger, IOptions<JwtConfiguration> configuration, IEmailService emailService, IConfiguration configurations)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _configuration = configuration;
            _jwtConfiguration = _configuration.Value;
            _emailService = emailService;
            _configurations = configurations;
        }
        public async Task<Response<LoginDTO>> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
                return new Response<LoginDTO>(false, "Authentication failed. Wrong user name or password.", null);

            if (!user.IsActive)
                return new Response<LoginDTO>(false, "Authentication failed. Account has been blocked.", "403");
            if (!await ValidateUser(loginRequest))
            {
                return new Response<LoginDTO>(false, "Authentication failed. Wrong user name or password.", null);
            }

            _mapper.Map(_user, _userResponse);
            var token = await CreateToken(populateExp: true);
            if (token == null)
                return new Response<LoginDTO>(false, "Authentication Error. User don't have any role, please create new account!", null);

            return new Response<LoginDTO>(true, new LoginDTO(token, _userResponse));
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        public async Task<BaseResponse> Register(RegisterRequest registerRequest)
        {
            try
            {
                var user = _mapper.Map<AppUser>(registerRequest);
                user.Description = registerRequest.Description;
                GetAvartarUser(user);
                user.CreatedAt = DateTime.Now;//await AddCodeNumber(user.Email, codeNumber);

            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
            {
                return new BaseResponse(false, result.Errors.ToList()[0].Description, result.Errors.ToList()[0].Code);
            }

            if (registerRequest.CategoryId == null)
            {
                await AddStudentRole(user);
            }

            if (registerRequest.CategoryId != null)
            {
                await AddInstructorRole(user);
            }

            var userResponse = _mapper.Map<UserDTO>(user);

            var roles = await _userManager.GetRolesAsync(user);
            userResponse.Role = string.Join(",", roles);

                string codeNumber = CreateCodeNumber().Result.ToString();
                bool isAddCodeNumber = await AddCodeNumber(user.Email, codeNumber);
                if (!isAddCodeNumber)
                {
                    return new Response<BaseResponse>(false, "Add Code Number went wrong!", null);
                }
                var isSendEmailConfirm = await SendEmailConfirm(user.Email, "Register", codeNumber);
                if (!isSendEmailConfirm.IsSuccess)
                {
                    return new Response<BaseResponse>(false, "Send Email went wrong!", null);
                }
                
                return new BaseResponse(true);

            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }
        /// <summary>
        /// condirm 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="codeNumber"></param>
        /// <returns></returns>
        public async Task<BaseResponse> Confirm(string email, string codeNumber)
        {
            var user = await _userManager.FindByEmailAsync(email);
            DateTime endTime = (user.UpdatedAt != null) ? user.UpdatedAt.Value.AddMinutes(3) : user.CreatedAt.AddMinutes(3);
            if (user.CodeNumber != codeNumber || DateTime.Now >= endTime)
            {
                return new Response<BaseResponse>(false, "Something went wrong!", null);
            }
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            //
            codeNumber = CreateCodeNumber().Result.ToString();
            await AddCodeNumber(user.Email, codeNumber);
            
            return new BaseResponse(true);
        }

        private static void GetAvartarUser(AppUser user)
        {
            user.Fullname = user.Fullname.Trim();
            string FirstLetter = user.Fullname.Split()[0][0].ToString();
            var lastIndex = user.Fullname.Split().Length - 1;
            string LastLetter = null;
            if (lastIndex != 0)
                LastLetter = user.Fullname.Split()[lastIndex][0].ToString();

            var avartarUrl = "https://i2.wp.com/ui-avatars.com/api/" + FirstLetter + LastLetter + "/300/ed2a26/fff";
            user.AvatarUrl = avartarUrl;
        }

        private async Task AddInstructorRole(AppUser user)
        {
            var InstructorRole = "Instructor";

            bool existRole = await _roleManager.RoleExistsAsync(InstructorRole);

            if (!existRole)
            {
                var role = new IdentityRole<Guid>();
                role.Name = InstructorRole;
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, InstructorRole);
        }
        private async Task AddStudentRole(AppUser user)
        {
            var InstructorRole = "Student";

            bool existRole = await _roleManager.RoleExistsAsync(InstructorRole);

            if (!existRole)
            {
                var role = new IdentityRole<Guid>();
                role.Name = InstructorRole;
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, InstructorRole);
        }

        public async Task<TokenDTO> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();

            var claims = await GetClaims();
            if (claims == null)
                return null;

            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            // reset token
            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(_user);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDTO(accessToken, refreshToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtConfiguration.Key)),
                ValidateLifetime = true,
                ValidIssuer = _jwtConfiguration.Issuer,
                ValidAudience = _jwtConfiguration.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out
           securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null ||
           !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        public async Task<bool> ValidateUser(LoginRequest userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email);
            var result = _user != null && await _userManager.CheckPasswordAsync(_user,
           userForAuth.Password);
            if (!result)
                _logger.LogWarning($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
            return result;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {

            var key = Encoding.UTF8.GetBytes(_jwtConfiguration.Key);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
                new Claim(ClaimTypes.Name, _user.UserName),
            };
            var roles = await _userManager.GetRolesAsync(_user);

            if (roles.Count == 0 || roles == null)
            {
                return null;
            }
            _userResponse.Role = roles[0].ToString();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;

        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
        List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken
            (
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        public async Task<Response<TokenDTO>> RefreshToken(TokenDTO tokenDto)
        {
            try
            {
                var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
                var user = await _userManager.FindByNameAsync(principal.Identity.Name);

                if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
                    new Response<TokenDTO>(false, "can't reset token", null);

                _user = user;

                var token = await CreateToken(populateExp: false);
                return new Response<TokenDTO>(true, token);

            }
            catch (Exception ex)
            {
                return new Response<TokenDTO>(false, ex.Message, null);
            }
        }
        //send email
        private async Task<Response<BaseResponse>> SendEmailConfirm(string email, string working, string codeNumber)
        {

            string subjects;
            string message;
            switch (working.ToUpper())
            {
                case "FORGET PASSWORD":
                    {
                        subjects = "FORGET PASSWORD";
                        message = $"<p>Your verification code {codeNumber}</a></p>";
                        break;
                    }
                case "REGISTER":
                    {
                        subjects = "CONFIRM REGISTRATION";
                        message = $"<p>Your verification code {codeNumber}</a></p>";
                        break;
                    }
                default: throw new Exception("Provider cannot find out");
            }
            var senderEmail = _configurations["SMTP:Sender"];
            var result = (await _emailService.SendEmailAsync(senderEmail, email, subjects, message));
            if(!result.IsSuccess)
            {
                return new Response<BaseResponse>(false);
            }
            
            return new Response<BaseResponse>(true);
        }
        //
        private async Task<bool> AddCodeNumber(string email, string codeNumber)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("No user associated with email");
            }
            user.CodeNumber = codeNumber;
            user.UpdatedAt = DateTime.Now;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        //
        private async Task<string> CreateCodeNumber()
        {
            Random random = new Random();
            string codeNumber = random.Next(1000, 9999).ToString();
            return codeNumber;
        }
        //
        public async Task<BaseResponse> ResetCodeNumber(string email, string working)
        {
            string codeNumber = CreateCodeNumber().Result.ToString();
            bool isAddCodeNumber = await AddCodeNumber(email, codeNumber);
            if (!isAddCodeNumber)
            {
                return new Response<BaseResponse>(false, "Add Code Number went wrong!", null);
            }
            var isSendEmailConfirm = await SendEmailConfirm(email, working, codeNumber);
            if (!isSendEmailConfirm.IsSuccess)
            {
                return new Response<BaseResponse>(false, "Send Email went wrong!", null);
            }
            return new BaseResponse(true);
        }
        /// <summary>
        /// Forgot Passworrd
        /// </summary>
        /// <param name="email">abc@gmail.com</param>
        /// <returns></returns>
        public async Task<Response<BaseResponse>> ForgetPassWord(string email, string originValue)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new Response<BaseResponse>(false, "No user associated with email", null);
                }
                string codeNumber = CreateCodeNumber().Result.ToString();
                bool isAddCodeNumber = await AddCodeNumber(user.Email, codeNumber);
                if (!isAddCodeNumber)
                {
                    return new Response<BaseResponse>(false, "Add Code Number went wrong!", null);
                }
                var isSendEmailConfirm = await SendEmailConfirm(user.Email, "Forget PassWord", codeNumber);
                if (!isSendEmailConfirm.IsSuccess)
                {
                    return new Response<BaseResponse>(false, "Send Email went wrong!", null);
                }
                return new Response<BaseResponse>(true, null, null);
            }
            catch (Exception ex)
            {
                return new Response<BaseResponse>(false, ex.Message, null);
            }
        }
        //
        public async Task<Response<BaseResponse>> ResetPassWord(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordRequest.email);
                if (user == null)
                {
                    return new Response<BaseResponse>(false, "No user associated with email", null);
                }
                if (user.CodeNumber != resetPasswordRequest.codeNumber || DateTime.Now >= user.UpdatedAt.Value.AddMinutes(3))
                {
                    return new Response<BaseResponse>(false, "Something went wrong!", null);
                }
                if (resetPasswordRequest.newPassword != resetPasswordRequest.comfirmPassword)
                {
                    return new Response<BaseResponse>(false, "Password doesn't match its confirmation", null);
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var Result = await _userManager.ResetPasswordAsync(user, token, resetPasswordRequest.newPassword);
                string codeNumber = CreateCodeNumber().Result.ToString();
                await AddCodeNumber(user.Email, codeNumber);
                if (!Result.Succeeded)
                {
                    return new Response<BaseResponse>(false, "Something went wrong!", null);
                }
                return new Response<BaseResponse>(true, "Password has been reset successfully", null);
            }
            catch (Exception ex)
            {
                return new Response<BaseResponse>(false, ex.Message, null);
            }
        }

        /// <summary>
        /// Login Google, Facebook, ... 
        /// </summary>
        /// <param name="externalLoginResquest"></param>
        /// <returns></returns>
        public async Task<Response<LoginDTO>> ExternalLogin(ExternalLoginResquest externalLoginResquest)
        {
            try
            {
                switch (externalLoginResquest.Provider.ToUpper())
                {
                    case "GOOGLE":
                        return new Response<LoginDTO>(true, await GoogleLogin(externalLoginResquest));
                    case "FACEBOOK":
                        return new Response<LoginDTO>(true, await FacebookLogin(externalLoginResquest));
                    default: throw new Exception("Provider cannot find out");
                }
            }
            catch (Exception ex)
            {
                return new Response<LoginDTO>(false, ex.Message, null);
            }
        }


        /// <summary>
        /// TODO: Handle Facebook login here!
        /// </summary>
        /// <param name="externalLoginResquest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<LoginDTO> FacebookLogin(ExternalLoginResquest externalLoginResquest)
        {

            string AppId = _configurations["Authentication:Facebook:AppId"];
            string AppSecret = _configurations["Authentication:Facebook:AppSecret"];
            var url = new Uri($"https://graph.facebook.com/oauth/access_token?client_id={AppId}&client_secret={AppSecret}&grant_type=client_credentials");


            // 1.generate an app access token
            var httpClient = new HttpClient();
            var appAccessTokenResponse = await httpClient.GetStringAsync(url);
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={externalLoginResquest.Token}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                throw new Exception();
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await httpClient.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={externalLoginResquest.Token}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new AppUser
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Fullname = userInfo.Name,
                    FacebookLink = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    AvatarUrl = userInfo.Picture.Data.Url
                };

                var result = await _userManager.CreateAsync(appUser);

                if (!result.Succeeded)
                    throw new Exception();
                await _userManager.AddToRoleAsync(appUser, UserRolesConstant.Student);
            }

            // generate the jwt for the local user...
            user = await _userManager.FindByNameAsync(userInfo.Email);
            user.Fullname = userInfo.Name;
            var userRole = await _userManager.GetRolesAsync(user);
            var token = await CreateToken(populateExp: true);
            user.UpdatedAt = DateTime.Now;
            await _userManager.UpdateAsync(user);
            UserDTO userWithRole = _mapper.Map<UserDTO>(user);
            userWithRole.Role = userRole.FirstOrDefault();

            return new LoginDTO(
               token,
               new UserDTO()
               {
                   FullName = user.Fullname,
                   Email = user.Email,
                   Role = UserRolesConstant.Student
               });
        }

        /// <summary>
        /// Login with Google 
        /// </summary>
        /// <param name="externalLoginResquest"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<LoginDTO> GoogleLogin(ExternalLoginResquest externalLoginResquest)
        {
            var payload = await VerifyGoogleToken(externalLoginResquest);
            if (payload is null)
            {
                throw new Exception("Payload is null");
            }
            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user is null)
            {
                var appUser = new AppUser()
                {
                    Id = new Guid(),
                    UserName = payload.Email,
                    Email = payload.Email,
                    Fullname = payload.Name,
                    EmailConfirmed = true,
                    AvatarUrl = payload.Picture
                };
                var result = await _userManager.CreateAsync(appUser);
                if (!result.Succeeded)
                    throw new Exception();
                await _userManager.AddToRoleAsync(appUser, UserRolesConstant.Student);
            }
            user = await _userManager.FindByEmailAsync(payload.Email);
            var userRoles = await _userManager.GetRolesAsync(user);
            // create token
            var token = await CreateToken(populateExp: true);
            return new LoginDTO(
                token,
                new UserDTO()
                {
                    FullName = user.Fullname,
                    Email = user.Email,
                    Role = UserRolesConstant.Student
                });

        }
        /// <summary>
        /// Verify Google Token
        /// </summary>
        /// <param name="externalAuth"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginResquest externalAuth)
        {
            try
            {
                var googleAuth = _configurations.GetSection("Authentication:Google");
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { googleAuth["ClientId"] }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.Token, settings);
                return payload;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
