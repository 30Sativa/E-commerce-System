using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EcommerceSystem.Infrastructure.Persistence;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using EcommerceSystem.Application.Validations.Auth;
using EcommerceSystem.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EcommerceSystem.Application.Interfaces.Repositories;
using EcommerceSystem.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using EcommerceSystem.Application.Mappings;
using EcommerceSystem.Infrastructure.Mappings;



var builder = WebApplication.CreateBuilder(args);

// ---------------- Controllers + FluentValidation ----------------
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginValidator>());
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

// ---------------- MediatR (CQRS) ----------------
// Quét toàn bộ assembly của Application
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ICustomerRepository).Assembly));
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(IProductRepository).Assembly));
// ---------------- AutoMapper ----------------
builder.Services.AddAutoMapper(typeof(CustomerAppProfile).Assembly,
                               typeof(CustomerInfraProfile).Assembly,
                               typeof(ProductAppProfile).Assembly,
                               typeof(ProductInfraProfile).Assembly);


// ---------------- API Behavior ----------------
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; // dùng middleware custom
});

// ---------------- Dependency Injection ----------------
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// ---------------- Swagger ----------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------- CORS ----------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ---------------- Authentication (JWT + Google) ----------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    })
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        googleOptions.CallbackPath = "/signin-google";
        googleOptions.SaveTokens = true; // lưu id_token
    });

// ---------------- Database (Postgres) ----------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------- Build App ----------------
var app = builder.Build();

// ---------------- Middleware Pipeline ----------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseMiddleware<ExceptionHandlingMiddleware>(); // custom exception → BaseResponse.FailResponse

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
