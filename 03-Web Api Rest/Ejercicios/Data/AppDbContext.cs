using Microsoft.EntityFrameworkCore;
using EjerciciosApi.Models;
namespace EjerciciosApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Aqui agregaremos los DbSets (tablas) mas adelante
        public DbSet<OrdenMantenimiento> OrdenesMantenimiento { get; set; }
        public DbSet<ControlCalidad> ControlesCalidad { get; set; }
        public DbSet<LoteProduccion> LotesProduccion { get; set; }
    }
}
