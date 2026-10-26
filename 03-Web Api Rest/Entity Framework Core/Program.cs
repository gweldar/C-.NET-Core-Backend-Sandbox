using Microsoft.EntityFrameworkCore;
using DammWebApi.Data;
var builder = WebApplication.CreateBuilder(args);

// 1. Registramos el DbContext configurando SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=damm.db"));

// 1. Registramos los controladores
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// 2. Mapeamos las rutas de los controladores
app.MapControllers();

app.Run();