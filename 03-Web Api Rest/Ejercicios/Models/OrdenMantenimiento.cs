namespace EjerciciosApi.Models
{
    public class OrdenMantenimiento
    {
        public int Id {get; set;}
        public string Equipo {get; set;} =string.Empty;
        public string Descripcion {get; set;}=string.Empty;  
        public int Prioridad {get; set;}
        public string Estado {get; set;}=string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}