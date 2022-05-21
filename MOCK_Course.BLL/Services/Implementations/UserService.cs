using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Course.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private const int TokenExpires = 24;
        public UserService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<Response<LoginResponse>> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (user is null)
                {
                    return new Response<LoginResponse>(false, "Email don't exist", null);
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, loginRequest.Remember, false);

                if (!result.Succeeded)
                    return new Response<LoginResponse>(false, "password don't correct", null);

                List<Claim> authClaims = await GetClaims(user);
                var token = GenerateAccessToken(authClaims);
                return new Response<LoginResponse>(true, new LoginResponse(token));
            }
            catch (Exception ex)
            {
                return new Response<LoginResponse>(false, ex.Message, null);
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
                user.Id = Guid.NewGuid();

                var result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (result.Succeeded)
                {
                    return new BaseResponse
                    {
                        IsSuccess = true,
                        Message = "Success"
                    };
                }
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = result.Errors.ToList()[0].Description
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Secret"]));
            var signCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:ValidIssuer"],
                audience: _configuration["AuthSettings:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(TokenExpires),
                signingCredentials: signCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
        private async Task<List<Claim>> GetClaims(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null) throw new Exception("Can't get role of user");

            var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, string.Join(";",roles))
                //new Claim(ClaimTypes.Role, "User")
                };
            return authClaims;
        }
    }
}
