namespace DammWebApi.Models
{
    public class Tanque
    {
        public int Id { get; set; }
        public string CodigoTanque { get; set; } = string.Empty;
        public decimal Temperatura { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}