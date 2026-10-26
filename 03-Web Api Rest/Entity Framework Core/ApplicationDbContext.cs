using Microsoft.EntityFrameworkCore;
using DammWebApi.Models;

namespace DammWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        // Esta propiedad se convertirá en la tabla "Turnos" en la Base de Datos
        public DbSet<Turno> Turnos { get; set; }
    }
}