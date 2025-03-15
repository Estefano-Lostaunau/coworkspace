using Coworkspace.Api.Models;
using Coworkspace.Api.DTOs;
using Coworkspace.Api.Repositories;
using Coworkspace.Api.Config;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Coworkspace.Api.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO> Register(RegisterDTO registerDto)
        {
            var existingUser = await _userRepository.GetUserByEmail(registerDto.Email);
            if (existingUser != null)
                throw new Exception("El usuario ya existe");

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            await _userRepository.AddUser(user);

            var token = GenerateJwtToken(user);
            return new AuthResponseDTO { Id = user.Id, Name = user.Name, Token = token };
        }

        public async Task<AuthResponseDTO?> Login(LoginDTO loginDto)
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                return null;

            var token = GenerateJwtToken(user);
            return new AuthResponseDTO { Id = user.Id, Name = user.Name, Token = token };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtConfig = _configuration.GetSection("JwtConfig").Get<JwtConfig>();
            if (jwtConfig == null)
                throw new Exception("Falta la configuración JWT");

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.ExpirationInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}