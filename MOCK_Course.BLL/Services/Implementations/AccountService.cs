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

namespace Course.BLL.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;
        //private readonly IConfiguration _configuration;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IOptions<JwtConfiguration> _configuration;
        private AppUser _user;
        private UserResponse _userResponse = new UserResponse();
        public AuthenticationService(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<Guid>> roleManager, ILogger<AuthenticationService> logger, IOptions<JwtConfiguration> configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _configuration = configuration;
            _jwtConfiguration = _configuration.Value;
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
    }
}
