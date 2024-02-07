using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Data.Models;
using TechDrop.Logic.Exceptions;

namespace TechDrop.Logic.Services;

/// <summary>
/// Сервис для получения <see cref="User"/> из контекста запроса.
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
    /// <returns></returns>
    /// <exception cref="InternalServerException">При HttpContext = null.</exception>
    /// <exception cref="UnauthorizedException">При Claim = null.</exception>
    public async Task<User> GetCurrentUser()
    {
        // Получаем контекст текущего запроса
        var context = _httpContextAccessor.HttpContext;

        if (context == null)
        {
            throw new InternalServerException();
        }
        
        // Получаем необходимые клеймы из Jwt-токена
        // TODO когда буду делать админа - надо прикрутить сюда роль (как минимум)
        var email = context.User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
        {
            throw new UnauthorizedException();
        }
        
        // Пытаемся найти пользователя в бд по имеющимся клеймам
        var user = await _dbContext.Users
            .Where(u => u.Email.Equals(email))
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new UnauthorizedException();
        }

        return user;
    }
}