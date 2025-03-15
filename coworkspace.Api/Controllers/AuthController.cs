using Microsoft.AspNetCore.Mvc;
using Coworkspace.Api.Services;
using Coworkspace.Api.DTOs;

namespace Coworkspace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterDTO registerDto)
        {
            var result = await _authService.Register(registerDto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO loginDto)
        {
            var result = await _authService.Login(loginDto);
            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}