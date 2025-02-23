using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceManagmentApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok($"You are authorized as user with id {User.FindFirstValue(ClaimTypes.NameIdentifier)}");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] UserLoginDTO request)
        {
            var response = await _authService.LoginAsync(request);

            return Ok(response);
        }

        [HttpPost("loginWithRefreshToken")]
        public async Task<ActionResult<AuthResponseDTO>> LoginWithRefreshToken([FromBody] string refreshToken)
        {
            var response = await _authService.RefreshTokenAsync(refreshToken);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register([FromBody] UserRegisterDTO request)
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }
    }
}
