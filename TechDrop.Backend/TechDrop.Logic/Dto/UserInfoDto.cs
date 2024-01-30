namespace TechDrop.Logic.Dto;

/// <summary>
/// Информация о пользователе, отдаваемая на клиент при успешной аутентификации
/// </summary>
public class UserInfoDto
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; } = null!;
    
    /// <summary>
    /// Токен доступа
    /// </summary>
    public string AccessToken { get; set; }
}