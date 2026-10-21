using Microsoft.AspNetCore.Mvc;

namespace DammWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        [HttpPost("evaluar-merma")]
        public IActionResult EvaluarMerma([FromBody] LoteInput input)
        {
            string estado;
            if (input.PorcentajeMermas >2m)
                estado = "Lote bajo REVISIÓN";
            else
                estado = "Lote APROBADO";
            
            return Ok(new
            {
                Mensaje = "Merma evaluada correctamente",
                Lote = input.CodigoLote,
                Mermas = input.PorcentajeMermas,
                Estado = estado 
            });
        }

        public class LoteInput
        {
            public string CodigoLote { get; set; }
            public decimal PorcentajeMermas { get; set; }
        }
    }
}
