using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DammWebApi.Data;
using DammWebApi.Models;

namespace DammWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos el DbContext a traves del constructor
        public LotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerLotePorId(int id)
        {
            // Consultamos la tabla Lotes de forma asíncrona usando EF Core
            var lote = await _context.Lotes.FindAsync(id);
    
            if (lote == null)
                return NotFound(new { Mensaje = "Lote no encontrado" });
    
            return Ok(lote);
        }

        [HttpGet("analizar")]
        public async Task<IActionResult> ObtenerLotes()
        {
            // Consultamos la tabla Lotes de forma asíncrona usando EF Core
            var lotes = await _context.Lotes.ToListAsync();
    
            return Ok(lotes);
        }
    
        [HttpPost("evaluar-merma")]
        public async Task<IActionResult> EvaluarMerma([FromBody] LoteInput input)
        {
            if (string.IsNullOrWhiteSpace(input.CodigoLote))
                return BadRequest(new { Mensaje = "El código de lote no puede estar vacío" });
            if (input.PorcentajeMermas < 0m)
                return BadRequest(new { Mensaje = "El porcentaje de merma no puede ser negativo" });
            
            
            string estado;
            if (input.PorcentajeMermas >2m)
                estado = "Lote bajo REVISIÓN";
            else
                estado = "Lote APROBADO";
            
            var nuevoLote = new Lote
            {
                CodigoLote = input.CodigoLote,
                PorcentajeMermas = input.PorcentajeMermas,
                Estado = estado,
                FechaRegistro = DateTime.Now
            };
            
            _context.Lotes.Add(nuevoLote);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Mensaje = "Lote evaluado y guardado en Base de Datos correctamente",
                Lote = nuevoLote,
            });
        }
    }
    public class LoteInput
    {
        public string CodigoLote { get; set; } = string.Empty;
        public decimal PorcentajeMermas { get; set; }
    }
}