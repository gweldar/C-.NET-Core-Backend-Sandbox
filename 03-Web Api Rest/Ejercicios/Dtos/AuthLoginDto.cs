namespace EjerciciosApi.Dtos
{
    public class AuthLoginDto
    {
        public string Email { get; set; } = string.Empty;
        private string Password { get; set; } = string.Empty;
    }
}