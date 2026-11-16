using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EjerciciosApi.Data;
using EjerciciosApi.Models;
using EjerciciosApi.Dtos;
namespace EjerciciosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MantenimientoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MantenimientoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<OrdenMantenimientoOutputDto>> CrearOrdenMantenimiento(OrdenMantenimientoInputDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Equipo))
                return BadRequest("El equipo no puede estar vacio");
            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                return BadRequest("La descripcion no puede estar vacia");
            if (dto.Prioridad <0 || dto.Prioridad > 3)
                return BadRequest("La prioridad debe ser un valor entre 0 y 3");
            var nuevoOrdenMantenimiento = new OrdenMantenimiento
            {
                Equipo = dto.Equipo,
                Descripcion = dto.Descripcion,
                Prioridad = dto.Prioridad,
                Estado ="Pendiente",
                FechaCreacion = DateTime.Now
            };
            _context.OrdenesMantenimiento.Add(nuevoOrdenMantenimiento);
            await _context.SaveChangesAsync();
            var outputDto = new OrdenMantenimientoOutputDto
            {
                Id = nuevoOrdenMantenimiento.Id,
                Equipo = nuevoOrdenMantenimiento.Equipo,
                Descripcion = nuevoOrdenMantenimiento.Descripcion,
                Prioridad = nuevoOrdenMantenimiento.Prioridad,
                Estado = nuevoOrdenMantenimiento.Estado,
                FechaCreacion = nuevoOrdenMantenimiento.FechaCreacion
            };
            return Ok(outputDto);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenMantenimientoOutputDto>>> GetOrdenesMantenimiento()
        {
            var ordenesDto =await _context.OrdenesMantenimiento
                .AsNoTracking()
                .Select(OrdenesMantenimiento => new OrdenMantenimientoOutputDto
                {
                    Id = OrdenesMantenimiento.Id,
                    Equipo = OrdenesMantenimiento.Equipo,
                    Descripcion = OrdenesMantenimiento.Descripcion,
                    Prioridad = OrdenesMantenimiento.Prioridad,
                    Estado = OrdenesMantenimiento.Estado,
                    FechaCreacion = OrdenesMantenimiento.FechaCreacion
                })
                .ToListAsync();
            return Ok(ordenesDto);
        }
        [HttpPut("{id}/estado")]
        public async Task<ActionResult<OrdenMantenimientoOutputDto>> CambiarEstadoOrdenMantenimiento(int id, [FromBody] OrdenMantenimientoUpdateDto dto)
        {
            if (!string.Equals(dto.RolUsuario,"Supervisor", StringComparison.OrdinalIgnoreCase))
                return Unauthorized(new { mensaje = "Acceso denegado: solo un Supervisor puede modificar el estado de la orden."});
            var estadoMantenimiento = await _context.OrdenesMantenimiento
                .FirstOrDefaultAsync(mantenimiento => mantenimiento.Id == id);
            if (estadoMantenimiento == null)
                return NotFound(new { mensaje = $"No se encontró la orden de mantenimiento con ID {id}" });
            if (string.IsNullOrWhiteSpace(dto.NuevoEstado))
                return BadRequest(new { mensaje = "El estado no puede estar vacío" });
            
            estadoMantenimiento.Estado = dto.NuevoEstado;
            await _context.SaveChangesAsync();
            var OutputDto = new OrdenMantenimientoOutputDto
            {
                Id = estadoMantenimiento.Id,
                Equipo = estadoMantenimiento.Equipo,
                Descripcion = estadoMantenimiento.Descripcion,
                Prioridad = estadoMantenimiento.Prioridad,
                Estado = estadoMantenimiento.Estado,
                FechaCreacion = estadoMantenimiento.FechaCreacion
            };
            return Ok(OutputDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrdenMantenimientoOutputDto>>  EliminarOrdenMantenimiento(int id, [FromQuery] string rolUsuario = "")
        {
            if (!string.Equals(rolUsuario,"Supervisor", StringComparison.OrdinalIgnoreCase))
                return Unauthorized(new { mensaje = "Acceso denegado: solo un Supervisor puede eliminar órdenes de mantenimiento"});
            var OrdenMantenimiento = await _context.OrdenesMantenimiento
                .FirstOrDefaultAsync(mantenimiento => mantenimiento.Id == id);
            if (OrdenMantenimiento == null)
                return NotFound(new { Mensaje = $"No se encontró la orden de mantenimiento con ID {id} para eliminar." });
             _context.OrdenesMantenimiento.Remove(OrdenMantenimiento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}