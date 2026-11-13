namespace EjerciciosApi.Dtos
{
    public class LoteProduccionUpdateDto
    {
        public string CodigoLote { get; set; } = string.Empty;
        public decimal CantidadLitros { get; set; }
    }
}