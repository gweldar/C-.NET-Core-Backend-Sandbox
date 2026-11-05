namespace EjerciciosApi.Models
{
    public class ControlCalidad
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal PH { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaInspeccion { get; set; } = DateTime.Now;
    }
}