using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DammWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TanquesController : ControllerBase
    {
        // Petición GET: api/tanques
        [HttpGet]
        public IActionResult ObtenerTanques()
        // Petición POST: api/tanques/evaluar
        {
            var listaTanques = new List<object>
            {
                new { Codigo = "TK-01", Temperatura = 12.5m, Estado = "ÓPTIMO" },
                new { Codigo = "TK-02", Temperatura = 22.0m, Estado = "ALERTA: Alta" },
                new { Codigo = "TK-03", Temperatura = 8.0m, Estado = "ALERTA: Baja" }
            };

            return Ok(listaTanques);
        }
        [HttpPost("evaluar")]
        public IActionResult EvaluarTanque([FromBody] TanqueInput input)
        {
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

    // Devolvemos la respuesta formateada
            return Ok(new
            {
                Mensaje = "Tanque evaluado correctamente",
                Codigo = input.CodigoTanque,
                Temperatura = input.Temperatura,
                Estado = estado
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