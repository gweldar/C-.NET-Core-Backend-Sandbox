using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DammWebApi.Data;
using DammWebApi.Models;

namespace DammWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos el DbContext a traves del constructor
        public TurnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("liquidar")]
        public async Task<IActionResult> LiquidarTurno([FromBody] TurnoInput input)
        {
            // Validaciones
            if (input.HorasTrabajadas <= 0m)
                return BadRequest(new { Mensaje = "Las horas trabajadas deben ser mayores a cero" });
            if (input.BotellasDefectuosas > input.BotellasProducidas)
                return BadRequest(new { Mensaje = "Las botellas defectuosas no pueden superar el total producido" });
            if (input.BotellasDefectuosas < 0)
                return BadRequest(new { Mensaje = "Las botellas defectuosas no pueden ser negativas" });
            if (input.BotellasProducidas < 0)
                return BadRequest(new { Mensaje = "Las botellas producidas no pueden ser negativas" });

            // Calculos
            int botellasBuenas = input.BotellasProducidas - input.BotellasDefectuosas;
            decimal rendimiento = (decimal)botellasBuenas / input.HorasTrabajadas;
            string estado;

            if (rendimiento >= 1000m)
                estado = "Rendimiento SOBRESALIENTE";
            else if (rendimiento >= 500m)
                estado = "Rendimiento NORMAL";
            else
                estado = "Rendimiento BAJO - Revisa linea";

            // Creacion de Turno para la Base de Datos
            var nuevoTurno = new Turno
            {
                Operario = input.Operario,
                BotellasProducidas = input.BotellasProducidas,
                BotellasDefectuosas = input.BotellasDefectuosas,
                HorasTrabajadas = input.HorasTrabajadas,
                BotellasBuenas = botellasBuenas,
                Rendimiento = rendimiento,
                Estado = estado,
                FechaRegistro = DateTime.Now
            };

            // Guardado en Base de Datos
            _context.Turnos.Add(nuevoTurno);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Mensaje = "Turno liquidado y guardado en Base de Datos correctamente",
                Turno = nuevoTurno
            });
        }
    }

    public class TurnoInput
    {
        public string Operario { get; set; } = string.Empty;
        public int BotellasProducidas { get; set; }
        public int BotellasDefectuosas { get; set; }
        public decimal HorasTrabajadas { get; set; }
    }
}