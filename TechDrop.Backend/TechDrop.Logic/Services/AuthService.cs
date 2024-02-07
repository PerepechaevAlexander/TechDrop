using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TechDrop.Logic.Configurations;
using TechDrop.Logic.Dto;
using TechDrop.Logic.Exceptions;

namespace TechDrop.Logic.Services;

/// <summary>
/// Сервис аутентификации
/// </summary>
public class AuthService
{
    private readonly AuthSettings _authSettings;

    public AuthService(AuthSettings authSettings)
    {
        _authSettings = authSettings;
    }
    
    /// <summary>
    /// Получить токен доступа для пользователя
    /// </summary>
    /// <param name="userInfoDto"><see cref="UserInfoDto"/></param>
    /// <returns></returns>
    public string GetAccessToken(UserInfoDto userInfoDto)
    {
        // Создаём клеймы пользователя на основе информации о нём
        // TODO когда буду делать админа - надо прикрутить сюда роль (как минимум)
        var userClaims = new List<Claim>
        {
            new(ClaimTypes.Email, userInfoDto.Email)
        };
        
        // Создаём токен доступа
        var jwt = new JwtSecurityToken(
            issuer: _authSettings.Issuer,
            audience: _authSettings.Audience,
            claims: userClaims,
            signingCredentials: new SigningCredentials(_authSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        
        // Записываем токен в строку
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        // Если не удалось записать токен -> кидаем ошибку 500
        if (accessToken.IsNullOrEmpty())
        {
            throw new InternalServerException("Ошибка аутентификации! Попробуйте ещё раз");
        }
        
        return accessToken;
    }
}