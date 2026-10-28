using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DammWebApi.Data;
using DammWebApi.Models;
namespace DammWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TanquesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos el DbContext a través del constructor
        public TanquesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Petición GET: api/tanques
        [HttpGet("analizar")]
        public async Task<IActionResult> ObtenerTanques()
        {
            // Consultamos la tabla Tanques de forma asíncrona usando EF Core
            var tanques = await _context.Tanques.ToListAsync();

            return Ok(tanques);
        }
        // Petición POST: api/tanques/evaluar
        [HttpPost("evaluar")]
        public async Task<IActionResult> EvaluarTanque([FromBody] TanqueInput input)
        {
            if (string.IsNullOrWhiteSpace(input.CodigoTanque))
                return BadRequest(new { Mensaje = "El código de tanque no puede estar vacío" });
    // Evaluamos la temperatura que nos llega desde Postman¡
            string estado;
            if (input.Temperatura < 10m && input.Temperatura > 0m)
                estado = "ALERTA: Temperatura demasiado baja";
            else if (input.Temperatura >= 10m && input.Temperatura <= 18m)
                estado = "ÓPTIMO: Temperatura en rango correcto";
            else if(input.Temperatura <= 0m)
                estado ="ALERTA CRÍTICA: Tanque Congelado";
            else
                estado = "ALERTA: Temperatura demasiado alta";

            var nuevoTanque = new Tanque
            {
                CodigoTanque = input.CodigoTanque,
                Temperatura = input.Temperatura,
                Estado = estado,
                FechaRegistro = DateTime.Now
            };

            _context.Tanques.Add(nuevoTanque);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Mensaje = "Tanque evaluado y guardado en Base de Datos correctamente",
                Tanque = nuevoTanque,
            });

        }

        // Clase auxiliar para recibir los datos del cuerpo (JSON)
        public class TanqueInput
        {
            public string CodigoTanque { get; set; }
            public decimal Temperatura { get; set; }
        }
    }
} 