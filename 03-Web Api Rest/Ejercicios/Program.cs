using Microsoft.EntityFrameworkCore;
using EjerciciosApi.Models;
using EjerciciosApi.Data;
// 1. CREACION DEL BUILDER (Configuracion del servidor y servicios)
var builder = WebApplication.CreateBuilder(args);

// --- AQUI REGISTRAMOS LOS SERVICIOS (Dependency Injection) ---
// Ejemplo: Controladores, DbContext, Swagger, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// 2. CONSTRUCCION DE LA APLICACION
var app = builder.Build();

// --- AQUI CONFIGURAMOS EL MIDDLEWARE (El tubo por donde pasan las peticiones HTTP) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Vincula las rutas HTTP a tus controladores

// 3. EJECUCION DEL SERVIDOR
app.Run();