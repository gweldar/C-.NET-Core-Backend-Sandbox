namespace EjerciciosApi.Dtos
{
    public class OrdenMantenimientoInputDto
    {
        public string Equipo {get; set;} =string.Empty;
        public string Descripcion {get; set;}=string.Empty;  
        public int Prioridad {get; set;}
    }
}