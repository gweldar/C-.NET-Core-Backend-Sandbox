namespace DammWebApi.Models
{
    public class Lote
    {
        public int Id { get; set; } // Clave primaria (Primary Key)
        public string CodigoLote { get; set; } = string.Empty;
        public decimal PorcentajeMermas { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}