using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.Common;
using EcommerceSystem.Application.DTOs.Requests.Auth;
using EcommerceSystem.Application.DTOs.Responses.Auth;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using EcommerceSystem.Application.Common.Exceptions;
using EcommerceSystem.Infrastructure.Persistence.Models;
using Google.Apis.Auth;

namespace EcommerceSystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ✅ Google Login
        public async Task<AuthResponse> GoogleLoginAsync(string idToken)
        {
            Console.WriteLine("📩 Received idToken: " + idToken?.Substring(0, 30) + "...");

            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _configuration["Authentication:Google:ClientId"] }
                });

                Console.WriteLine($"✅ Google payload: Email={payload.Email}, Name={payload.Name}, Audience={string.Join(",", payload.Audience)}");

                // 2. Kiểm tra user trong DB
                var user = await _context.Customers.FirstOrDefaultAsync(u => u.Email == payload.Email);

                if (user == null)
                {
                    Console.WriteLine("ℹ️ New Google user -> creating...");
                    user = new Customer
                    {
                        Name = payload.Name ?? payload.Email,
                        Email = payload.Email,
                        Authprovider = "Google",
                        Role = "Customer",
                        Createdat = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
                    };

                    _context.Customers.Add(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine($"ℹ️ Existing user found, provider={user.Authprovider}");
                    if (user.Authprovider == "Local")
                    {
                        user.Authprovider = "Local+Google";
                        _context.Customers.Update(user);
                        await _context.SaveChangesAsync();
                    }
                }

                // 3. Tạo JWT token
                return GenerateJwtToken(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Google token validation failed: " + ex.Message);
                throw new BusinessException("Google login validation failed: " + ex.Message);
            }
        }

        // ✅ Local Login
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            Console.WriteLine("📩 Login attempt: " + request.Email);

            var user = await _context.Customers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                Console.WriteLine("❌ Invalid email");
                throw new BusinessException("Invalid email");
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.Passwordhash);

            if (!isValidPassword)
            {
                Console.WriteLine("❌ Invalid password");
                throw new BusinessException("Invalid password");
            }

            Console.WriteLine("✅ Login success: " + user.Email);
            return GenerateJwtToken(user);
        }

        // ✅ Register
        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            Console.WriteLine("📩 Register attempt: " + request.Email);

            var existingUser = await _context.Customers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                Console.WriteLine("❌ Email already in use");
                throw new BusinessException("Email already in use");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Passwordhash = passwordHash,
                Authprovider = "Local",
                Role = "Customer",
                Createdat = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
            };

            _context.Customers.Add(newUser);
            await _context.SaveChangesAsync();

            Console.WriteLine("✅ Register success: " + newUser.Email);
            return true;
        }

        // ✅ Helper: Generate JWT Token
        private AuthResponse GenerateJwtToken(Customer user)
        {
            var secret = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(secret))
                throw new Exception("JWT Key is missing in configuration!");

            var key = Encoding.UTF8.GetBytes(secret);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Customerid.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? "Customer")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            Console.WriteLine("✅ JWT generated for user: " + user.Email);

            return new AuthResponse
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires.Value
            };
        }
    }
}
