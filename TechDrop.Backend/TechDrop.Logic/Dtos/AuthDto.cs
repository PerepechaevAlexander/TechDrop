namespace TechDrop.Logic.Dtos;

public class AuthDto
{
    /// <summary>
    /// Email юзера
    /// </summary>
    public string Email { get; set; } = null!;
    /// <summary>
    /// Пароль юзера
    /// </summary>
    public string Password { get; set; } = null!;
}