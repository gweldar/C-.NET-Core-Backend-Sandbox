using Microsoft.EntityFrameworkCore;
using EjerciciosApi.Models;
using EjerciciosApi.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
// 1. CREACION DEL BUILDER (Configuracion del servidor y servicios)
var builder = WebApplication.CreateBuilder(args);

// --- AQUI REGISTRAMOS LOS SERVICIOS (Dependency Injection) ---
// Ejemplo: Controladores, DbContext, Swagger, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// 1. Registrar ASP.NET Core Identity (Usuarios y Roles en SQLite)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// 2. Registrar la autenticación mediante tokens JWT
var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
// 2. CONSTRUCCION DE LA APLICACION
// 2. CONSTRUCCION DE LA APLICACION
var app = builder.Build();

// --- AQUI CONFIGURAMOS EL MIDDLEWARE (El tubo por donde pasan las peticiones HTTP) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Vincula las rutas HTTP a tus controladores

// 3. EJECUCION DEL SERVIDOR
app.Run();
