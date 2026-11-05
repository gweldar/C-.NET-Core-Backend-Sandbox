using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EjerciciosApi.Data;
using EjerciciosApi.Models;

namespace EjerciciosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControlCalidadController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ControlCalidadController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost] // POST: api/ControlCalidad ponemos los datos a la base de datos
        public async Task<IActionResult> ControlCalidad([FromBody] ControlCalidadInput input)
        {
            if (string.IsNullOrWhiteSpace(input.NombreProducto))
                return BadRequest(new { Message = "El nombre del producto es obligatorio." });
            string estado;
            if (input.PH >=3.8m && input.PH <=4.5m)
                estado = "CALIDAD APROBADA";
            else
                estado = "FUERA DE ESPECIFICACIÓN";
            
            var nuevoControlCalidad = new ControlCalidad
            {
                NombreProducto = input.NombreProducto,
                PH = input.PH,
                Estado = estado,
                FechaInspeccion = DateTime.Now
            };
            _context.ControlesCalidad.Add(nuevoControlCalidad);
            await _context.SaveChangesAsync();
            return Ok (new
            {
                mensaje = "Control de calidad registrado exitosamente.",
                controlCalidad = nuevoControlCalidad
            });
        }
        [HttpGet] // GET: api/ControlCalidad obtenemos todos los registros de la base de datos
        public async Task<IActionResult> GetControlesCalidad()
        {
            var ControlesCalidad = await _context.ControlesCalidad.ToListAsync();
            return Ok(ControlesCalidad);
        }
        [HttpGet("{id}")] // GET: api/ControlCalidad/{id} obtenemos un registro por su id
        public async Task<IActionResult> searchControlesCalidad(int id)
        {
            var ControlesCalidad = await _context.ControlesCalidad.FindAsync(id);
            if (ControlesCalidad== null)
                return NotFound(new {Message = "Control de calidad no encontrado."});

            return Ok(ControlesCalidad);
        }
        [HttpPut("{id}")] // PUT: api/ControlCalidad/{id} actualizamos un registro por su id
        public async Task<IActionResult> ActualizarControl(int id, [FromBody] ControlCalidadInput input)
        {
            var controlExistente = await _context.ControlesCalidad.FindAsync(id);
            // Validamos si el control de calidad existe
            if (controlExistente == null)
                return NotFound(new { Mensaje = $"No se encontró el control de calidad con ID {id}." });
            //vuelvo a validar todo lo que valido en el post ya que lo voy a volver a postear
            else if (string.IsNullOrWhiteSpace(input.NombreProducto))
                return BadRequest(new { Mensaje = "El nombre del producto es obligatorio." });
            string estado;
            if (input.PH >=3.8m && input.PH <=4.5m)
                estado = "CALIDAD APROBADA";
            else
                estado = "FUERA DE ESPECIFICACIÓN";
            // Actualizamos los campos del control de calidad existente
            controlExistente.NombreProducto = input.NombreProducto;
            controlExistente.PH = input.PH;
            controlExistente.Estado = estado;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Mensaje = "Control de calidad actualizado exitosamente.",
                ControlCalidad = controlExistente
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarControl(int id)
        {
            var controlExistente = await _context.ControlesCalidad.FindAsync(id);
            if (controlExistente == null)
            {
                return NotFound(new { Mensaje = $"No se encontró el control de calidad con ID {id} para eliminar." });
            }
            // .Remove me lo elimina y se lo mando a la bd desde _context
            _context.ControlesCalidad.Remove(controlExistente);
            // Guardamos los cambios en la base de datos
            await _context.SaveChangesAsync();
            return Ok(new { Mensaje = $"Control de calidad con ID {id} eliminado correctamente." });
        }
    }
    public class ControlCalidadInput
    {
        public string NombreProducto { get; set; } = string.Empty;
        public decimal PH { get; set; }
    }
}