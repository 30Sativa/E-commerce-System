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
using EcommerceSystem.Application.Mappings;
using EcommerceSystem.Infrastructure.Mappings;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using EcommerceSystem.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// ---------------- Controllers + FluentValidation ----------------
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginValidator>());
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// ---------------- MediatR (CQRS) ----------------
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ICustomerRepository).Assembly));
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(IProductRepository).Assembly));
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ICategoryRepository).Assembly));
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(IOrderRepository).Assembly));

// Fix for CS0121: Specify the generic overload explicitly to resolve ambiguity
builder.Services.AddAutoMapper(
    typeof(CustomerAppProfile),
    typeof(CustomerInfraProfile),
    typeof(ProductAppProfile),
    typeof(ProductInfraProfile),
    typeof(CategoryAppProfile),
    typeof(CategoryInfraProfile),
    typeof(OrderAppProfile),
    typeof(OrderInfraProfile),
    typeof(VoucherAppProfile),
    typeof(VoucherInfraProfile)
);




// ---------------- API Behavior ----------------
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; // dùng middleware custom
});

// ---------------- Redis ----------------
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "ECommerce_";
});

// ---------------- Dependency Injection ----------------
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();

// ---------------- Swagger ----------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------- CORS ----------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
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

app.UseCors("AllowFrontend");

app.UseMiddleware<ExceptionHandlingMiddleware>(); // custom exception → BaseResponse.FailResponse

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
