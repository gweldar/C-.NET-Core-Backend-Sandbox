namespace EjerciciosApi.Dtos
{
    public class LoteProduccionOutputDto
    {
        public int Id { get; set; }
        public string CodigoLote { get; set; } = string.Empty;
        public decimal CantidadLitros { get; set; }  
        public bool Aprobado { get; set; }
    }
}