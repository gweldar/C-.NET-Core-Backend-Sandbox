namespace EjerciciosApi.Models
{
    public class LoteProduccion
    {
        public int Id {get; set;}
        public string CodigoLote {get; set;} = string.Empty;
        public decimal CantidadLitros { get; set; }  
        public bool Aprobado { get; set; }
        public DateTime FechaFabricacion { get; set; } = DateTime.Now;
    }
}