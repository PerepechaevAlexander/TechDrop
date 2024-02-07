namespace TechDrop.Logic.Dto;

/// <summary>
/// Клеймы пользователя из Jwt-токена.
/// </summary>
public class UserClaimsDto
{
    /// <summary>
    /// Email пользователя.
    /// </summary>
    public string Email { get; init; } = null!;
}