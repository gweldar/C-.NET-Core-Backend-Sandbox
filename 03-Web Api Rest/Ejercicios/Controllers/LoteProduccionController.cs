using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EjerciciosApi.Data;
using EjerciciosApi.Models;
using EjerciciosApi.Dtos;
namespace EjerciciosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoteProduccionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LoteProduccionController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<LoteProduccionOutputDto>> Crear(LoteProduccionInputDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CodigoLote))
                return BadRequest(new {Message = "El código del lote es obligatorio."});
            else if (dto.CantidadLitros <0)
                return BadRequest(new {Message = "La cantidad de litros no puede ser negativa"});
            
            bool aprobado = dto.CantidadLitros >= 100;
            var nuevoLoteProduccion = new LoteProduccion
            {
                CodigoLote = dto.CodigoLote,
                CantidadLitros = dto.CantidadLitros,
                Aprobado = aprobado,
                FechaFabricacion = DateTime.UtcNow
            };
            
            _context.LotesProduccion.Add(nuevoLoteProduccion);
            await _context.SaveChangesAsync();
            
            var outputDto = new LoteProduccionOutputDto
            {
                Id = nuevoLoteProduccion.Id,
                CodigoLote = nuevoLoteProduccion.CodigoLote,
                CantidadLitros = nuevoLoteProduccion.CantidadLitros,
                Aprobado = nuevoLoteProduccion.Aprobado
            };
            return Ok(outputDto);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoteProduccionOutputDto>>> GetLotesProduccion()
        {
            var lotesDto = await _context.LotesProduccion
                .AsNoTracking()
                .Select(lote => new LoteProduccionOutputDto
                {
                    Id = lote.Id,
                    CodigoLote = lote.CodigoLote,
                    CantidadLitros = lote.CantidadLitros,
                    Aprobado = lote.Aprobado
                })
                .ToListAsync();
            return Ok(lotesDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LoteProduccionOutputDto>> SearchLotesProduccion(int id)
        {
            var lotesDto = await _context.LotesProduccion
                .AsNoTracking()
                .Where(lote => lote.Id == id)
                .Select(lote => new LoteProduccionOutputDto
                {
                    Id = lote.Id,
                    CodigoLote = lote.CodigoLote,
                    CantidadLitros = lote.CantidadLitros,
                    Aprobado = lote.Aprobado
                })
                .FirstOrDefaultAsync();
            if (lotesDto == null)
                return NotFound(new {Message = "Lote de producción no encontrado."});
                
            return Ok(lotesDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<LoteProduccionOutputDto>> UpdateLotesProduccion(int id, LoteProduccionUpdateDto dto)
        {
            var loteExistente = await _context.LotesProduccion
                .FirstOrDefaultAsync(lote => lote.Id == id);
            if (loteExistente == null)
                return NotFound(new {Message = "Lote de producción no encontrado."});
                
            if (string.IsNullOrWhiteSpace(dto.CodigoLote))
                return BadRequest(new {Message = "El código del lote es obligatorio."});
            else if (dto.CantidadLitros <0)
                return BadRequest(new {Message = "La cantidad de litros no puede ser negativa"});
            
            loteExistente.CodigoLote = dto.CodigoLote;
            loteExistente.CantidadLitros = dto.CantidadLitros;
            loteExistente.Aprobado = false; // Reiniciamos el estado de aprobación al actualizar que el equipo de control de calidad revise nuevamente el lote.
            await _context.SaveChangesAsync();
            
            var outputDto = new LoteProduccionOutputDto
            {
                Id = loteExistente.Id,
                CodigoLote = loteExistente.CodigoLote,
                CantidadLitros = loteExistente.CantidadLitros,
                Aprobado = loteExistente.Aprobado
            };
            return Ok(outputDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLotesProduccion(int id)
        {
            var loteExistente = await _context.LotesProduccion
                .FirstOrDefaultAsync(lote => lote.Id == id);
            if (loteExistente == null)
                return NotFound(new { Mensaje = $"No se encontró el lote de producción con ID {id} para eliminar." });
            // .Remove me lo elimina y se lo mando a la bd desde _context
            _context.LotesProduccion.Remove(loteExistente);
            // Guardamos los cambios en la base de datos
            await _context.SaveChangesAsync();
            return NoContent(); // Retorna un código de estado 204 Si se prefiere en el front end se puede añadir mensaje de confirmacion.
        }
    }
} 