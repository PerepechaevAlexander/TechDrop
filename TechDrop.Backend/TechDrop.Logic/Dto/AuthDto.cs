namespace TechDrop.Logic.Dto;

/// <summary>
/// Ифнормация о пользователе, необходимая для его аутентификации
/// </summary>
public class AuthDto
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; } = null!;
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; } = null!;
}