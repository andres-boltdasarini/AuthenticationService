using AuthenticationService.DAL.Repositories;
using AuthenticationService.BLL.Interfaces;
using AuthenticationService.BLL.Services;
using AuthenticationService.BLL.Mapping;
using AuthenticationService.PLL.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(BllMappingProfile));

// Register dependencies
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = redirectContext =>
            {
                redirectContext.HttpContext.Response.StatusCode = 401;
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Администратор"));
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); // Должно быть после HttpsRedirection

// Authentication & Authorization ДОЛЬШЕ быть до кастомных middleware
app.UseAuthentication();
app.UseAuthorization();

// Кастомные middleware после auth
app.UseLoggingMiddleware();
app.UseExceptionHandlingMiddleware();

app.MapControllers();

app.Run();