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
using BCrypt.Net;
using EcommerceSystem.Infrastructure.Persistence.Models;
using BCrypt.Net;

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

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if(user == null)
            {
                throw new BusinessException("Invalid email");
            }

            // TODO: Sau này dùng BCrypt để check password hash
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.Passwordhash);

            if(!isValidPassword)
            {
                throw new BusinessException("Invalid password");
            }
            // 🔹 Check JWT config
            var secret = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(secret))
                throw new Exception("JWT Key is missing in configuration!");

            // ✅ Dùng lại secret đã check, không đọc lại từ config
            var key = Encoding.UTF8.GetBytes(secret);

            var claim = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, user.Customerid.ToString()),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Role, user.Role ?? "Customer")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires.Value
            };
        }


        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            // Check exsiting email
            var existingUser = await _context.Customers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                throw new BusinessException("Email already in use");
            }
            //Hash password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            //Create new user
            var newUser = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Passwordhash = passwordHash,
                Authprovider = "Local",
                Role = "Customer",
                Createdat = DateTime.Now
            };

            _context.Customers.Add(newUser);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
