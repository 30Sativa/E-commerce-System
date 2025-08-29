using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Requests.Auth;
using EcommerceSystem.Application.DTOs.Responses.Auth;

namespace EcommerceSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);

        Task<AuthResponse> GoogleLoginAsync(string idToken);
    }
}
