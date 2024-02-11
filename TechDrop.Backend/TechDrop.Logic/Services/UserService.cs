using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Data.Models;
using TechDrop.Logic.Dto;
using TechDrop.Logic.Exceptions;

namespace TechDrop.Logic.Services;

/// <summary>
/// Сервис для работы с текущим пользователем <see cref="User"/>,
/// содержит get\set\check для <see cref="User"/> и его свойств.
/// </summary>
public class UserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TechDropDbContext _dbContext;

    public UserService(IHttpContextAccessor httpContextAccessor, TechDropDbContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Получить текущего пользователя.
    /// </summary>
    /// <param name="cancellationToken">токен отмены.</param>
    public async Task<User?> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userClaimsDto = GetUserClaims();
        
        return await _dbContext.Users
            .Where(u => u.Email.Equals(userClaimsDto.Email))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    /// <summary>
    /// Получить Id текущего пользователя.
    /// </summary>
    /// <param name="cancellationToken">токен отмены.</param>
    public async Task<int> GetCurrentUserId(CancellationToken cancellationToken)
    {
        var userClaimsDto = GetUserClaims();
        
        return await _dbContext.Users
            .Where(u => u.Email.Equals(userClaimsDto.Email))
            .Select(u => u.UserId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Получить клеймы пользователя.
    /// </summary>
    /// <returns><see cref="UserClaimsDto"/></returns>
    /// <exception cref="InternalServerException">если HttpContext = null.</exception>
    private UserClaimsDto GetUserClaims()
    {
        // Получаем контекст текущего запроса
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            throw new InternalServerException();
        }
        
        // Получаем клеймы из Jwt-токена
        // TODO когда буду делать админа - прикрутить сюда роль (как минимум)
        return new UserClaimsDto
        {
            Email = context.User.FindFirst(ClaimTypes.Email)?.Value!
        };
    }
}