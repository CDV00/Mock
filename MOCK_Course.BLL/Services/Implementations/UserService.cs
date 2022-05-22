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
        public UserService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
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

                var userResponse = _mapper.Map<UserResponse>(user);

                List<Claim> authClaims = await GetClaims(user, userResponse);
                var token = GenerateAccessToken(authClaims);
                return new Response<LoginResponse>(true, new LoginResponse(token, userResponse));
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
        public async Task<Response<UserResponse>> Register(RegisterRequest registerRequest)
        {
            try
            {
                var user = _mapper.Map<AppUser>(registerRequest);

                var result = await _userManager.CreateAsync(user, registerRequest.Password);

                var userResponse = _mapper.Map<UserResponse>(user);

                if (result.Succeeded)
                {
                    return new Response<UserResponse>(true, userResponse);
                }
                return new Response<UserResponse>(false, result.Errors.ToList()[0].Description, null);
            }
            catch (Exception ex)
            {
                return new Response<UserResponse>(false, ex.Message, null);
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
        private async Task<List<Claim>> GetClaims(AppUser user, UserResponse userResponse)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Count > 0)
            {
                userResponse.IsHaveRole = true;
            }

            var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, string.Join(";",roles))
                //new Claim(ClaimTypes.Role, "User")
                };
            return authClaims;
        }
    }
}
