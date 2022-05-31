using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DataTransferObjects;
using Course.BLL.Requests;
using Course.BLL.DTO;
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
using Microsoft.AspNetCore.Mvc;
using static Course.BLL.Responses.FacebookApiResponse;


namespace Course.BLL.Services.Implementations
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
        private UserResponse _userResponse = new UserResponse();
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
            try
            {
                if (!await ValidateUser(loginRequest))
                {
                    return new Response<LoginDTO>(false, "Email don't exist", null);
                }

                _mapper.Map(_user, _userResponse);
                var token = await CreateToken(populateExp: true);

                //var userResponse = _mapper.Map<UserResponse>(_user);

                //var roles = await _userManager.GetRolesAsync(_user);

                return new Response<LoginDTO>(true, new LoginDTO(token, _userResponse));
            }
            catch (Exception ex)
            {
                return new Response<LoginDTO>(false, ex.Message, null);
            }
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

                var result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (!result.Succeeded)
                {
                    return new BaseResponse(false, result.Errors.ToList()[0].Description, null);
                }

                if (registerRequest.CategoryId == null)
                {
                    await AddStudentRole(user);
                }

                if (registerRequest.CategoryId != null)
                {
                    await AddInstructorRole(user);
                }

                var userResponse = _mapper.Map<UserResponse>(user);

                var roles = await _userManager.GetRolesAsync(user);
                userResponse.Role = string.Join(",", roles);

                return new BaseResponse(true);

            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
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

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();

            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            // reset token
            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(_user);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto(accessToken, refreshToken);
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
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user,
           userForAuth.Password));
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

        public async Task<Response<TokenDto>> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
                new Response<TokenDto>(false, "can't reset token", null);

            _user = user;

            var token = await CreateToken(populateExp: false);
            return new Response<TokenDto>(true, token);
        }
        /// <summary>
        /// Forgot Passworrd
        /// </summary>
        /// <param name="email">abc@gmail.com</param>
        /// <returns></returns>
        public async Task<Response<BaseResponse>> ForgetPassWord(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new Response<BaseResponse>(false, "No user associated with email", null);
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var encodedToken = Encoding.UTF8.GetBytes(token);
                //var validToken = WebEncoders.Base64UrlEncode(encodedToken);
                //_userManager.toke
                // From Address
                var senderEmail = _configurations["SMTP:Sender"];
                //string url = $"{_configurations["AppUrl"]}/ResetPassword?email={email}&token={validToken}";
                string url = $"http://localhost:3000/reset-pass?email={email}&token={token}";
                await _emailService.SendEmailAsync(senderEmail, user.Email, "FORGET PASSWORD", "<h1>Follow the instruction to reset your password</h1>" + $"<p>To reset your password <a href={url}> click here.</a></p>");
      
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
                if (resetPasswordRequest.newPassword != resetPasswordRequest.comfirmPassword)
                {
                    return new Response<BaseResponse>(false, "Password doesn't match its confirmation", null);
                }
                var Result = await _userManager.ResetPasswordAsync(user, resetPasswordRequest.token, resetPasswordRequest.newPassword);
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
            // 1.generate an app access token
            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configurations["AuthSettings:Facebook:AppId"]}&client_secret={_configurations["AuthSettings:Facebook:AppSecret"]}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={externalLoginResquest.Token}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                throw new Exception();
                //return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid facebook token.", ModelState));
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v14.0/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={externalLoginResquest.Token}");
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
                    UserName = userInfo.Email
                    //Image = userInfo.Picture.Data.Url
                };

                var result = await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray())[..8]);

                if (!result.Succeeded)
                    throw new Exception();
                await _userManager.AddToRoleAsync(appUser, UserRoles.Student);
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
               new UserResponse()
               {
                   FullName = user.Fullname,
                   Email = user.Email,
                   Role = UserRoles.Student
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
                user = new AppUser()
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    Fullname = payload.Name,
                    EmailConfirmed = true,
                    //Image = payload.Picture,
                };
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, UserRoles.Student);
            }
            user = await _userManager.FindByEmailAsync(payload.Email);
            var userRoles = await _userManager.GetRolesAsync(user);
            // create token
            var token = await CreateToken(populateExp: true);
            return new LoginDTO(
                token,
                new UserResponse()
                {
                    FullName = user.Fullname,
                    Email = user.Email,
                    Role = UserRoles.Student
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
                var googleAuth = _configurations.GetSection("AuthSettings:Google");

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
