namespace DammWebApi.Models
{
    public class Turno
    {
        public int Id { get; set; } // Clave primaria (Primary Key)
        public string Operario { get; set; } = string.Empty;
        public int BotellasProducidas { get; set; }
        public int BotellasDefectuosas { get; set; }
        public decimal HorasTrabajadas { get; set; }
        public int BotellasBuenas { get; set; }
        public decimal Rendimiento { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}