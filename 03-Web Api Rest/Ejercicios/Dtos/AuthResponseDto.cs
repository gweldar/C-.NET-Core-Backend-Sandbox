namespace EjerciciosApi.Dtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; }
        public string Rol { get; set; } = string.Empty;
    }
}