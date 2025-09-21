using EcommerceSystem.Application.Common;
using EcommerceSystem.Application.Common.Exceptions;
using EcommerceSystem.Application.DTOs.Requests.Auth;
using EcommerceSystem.Application.DTOs.Responses.Auth;
using EcommerceSystem.Application.Features.Auth.Commands;
using EcommerceSystem.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _mediator.Send(new LoginCommand(request));
            return Ok(BaseResponse<AuthResponse>.SuccessResponse(response, "Login success"));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _mediator.Send(new RegisterCommand(request));
            return Ok(BaseResponse<bool>.SuccessResponse(response, "Register success"));
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                var response = await _mediator.Send(new GoogleLoginCommand(request.IdToken));
                return Ok(BaseResponse<AuthResponse>.SuccessResponse(response, "Google login success"));
            }
            catch (BusinessException ex)
            {
                // 400 Bad Request cho lỗi nghiệp vụ
                return BadRequest(BaseResponse<string>.FailResponse(ex.Message));
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error cho lỗi hệ thống
                return StatusCode(500, BaseResponse<string>.FailResponse("Google login error: " + ex.Message));
            }
        }
    }
}
