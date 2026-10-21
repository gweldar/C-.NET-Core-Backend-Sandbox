using Microsoft.AspNetCore.Mvc;

namespace DammWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnosController : ControllerBase
    {
        [HttpPost("liquidar")]
        public IActionResult LiquidarTurno([FromBody] TurnoInput input)
        {
            if (input.HorasTrabajadas <= 0m)
                return BadRequest(new { Mensaje = "Las horas trabajadas deben ser mayores a cero" });
            else if (input.BotellasDefectuosas > input.BotellasProducidas)
                return BadRequest(new { Mensaje = "Las botellas defectuosas no pueden superar el total producido" });
            else if (input.BotellasDefectuosas < 0)
                return BadRequest(new { Mensaje = "Las botellas defectuosas no pueden ser negativas" });
            else if (input.BotellasProducidas < 0)
                return BadRequest(new { Mensaje = "Las botellas producidas no pueden ser negativas" });
            else
            {
                int BotellasBuenas = input.BotellasProducidas - input.BotellasDefectuosas;
                decimal Rendimiento = (decimal)BotellasBuenas / input.HorasTrabajadas;
                string estado;
                if (Rendimiento >= 1000m)
                    estado = "Rendimiento SOBRESALIENTE";
                else if (Rendimiento >= 500m)
                    estado= "Rendimiento NORMAL";
                else
                    estado = "Rendimiento BAJO - Revisa línea";
                return Ok(new
                {
                    Mensaje = "Rendimiento evaluado correctamente",
                    Operario = input.Operario,
                    BotellasBuenas = BotellasBuenas,
                    Rendimiento = Rendimiento,
                    Estado = estado
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
}