using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responsesnamespace;
using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Course.BLL.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private const int TokenExpires = 24;
        public AccountService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
        public async Task<Response<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            try
            {
                var user = _mapper.Map<AppUser>(registerRequest);
                user.Description = registerRequest.Description;

                var result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (!result.Succeeded)
                {
                    return new Response<RegisterResponse>(false, result.Errors.ToList()[0].Description, null);
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

                List<Claim> authClaims = await GetClaims(user, userResponse);
                var token = GenerateAccessToken(authClaims);

                return new Response<RegisterResponse>(true, new RegisterResponse(token, userResponse));

            }
            catch (Exception ex)
            {
                return new Response<RegisterResponse>(false, ex.Message, null);
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
            userResponse.Role = string.Join(",", roles); 

            var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, string.Join(",",roles))
                //new Claim(ClaimTypes.Role, "User")
                };

            return authClaims;
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
    }
}
