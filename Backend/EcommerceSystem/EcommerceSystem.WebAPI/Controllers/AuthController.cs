using EcommerceSystem.Application.Common;
using EcommerceSystem.Application.DTOs.Requests.Auth;
using EcommerceSystem.Application.DTOs.Responses.Auth;
using EcommerceSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.WebAPI.Controllers
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(BaseResponse<AuthResponse>.SuccessResponse(response, "Login success"));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(BaseResponse<bool>.SuccessResponse(response, "Register success"));
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var response = await _authService.GoogleLoginAsync(request.IdToken);
            return Ok(BaseResponse<AuthResponse>.SuccessResponse(response, "Google login success"));
        }
    }
}
