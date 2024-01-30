using MediatR;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Logic.Dto;
using TechDrop.Logic.Exceptions;
using TechDrop.Logic.Services;

namespace TechDrop.Logic.Queries;

/// <summary>
/// Авторизация пользователя
/// </summary>
public class LoginQuery : IRequest<UserInfoDto>
{
    public string Email { get; }
    public string Password { get; }

    public LoginQuery(AuthDto authDto)
    {
        Email = authDto.Email;
        Password = authDto.Password;
    }
}

public class LoginQueryHandler : IRequestHandler<LoginQuery, UserInfoDto>
{
    private readonly TechDropDbContext _dbContext;
    private readonly AuthService _authService;

    public LoginQueryHandler(TechDropDbContext dbContext, AuthService authService)
    {
        _dbContext = dbContext;
        _authService = authService;
    }
    
    public async Task<UserInfoDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        // Пытаемся найти пользователя
        var userInfoDto = await _dbContext.Users
            .Where(u => u.Email.Equals(request.Email) && u.Password.Equals(request.Password))
            .Select(u => new UserInfoDto
            {
                Email = u.Email
            }).FirstOrDefaultAsync(cancellationToken);
        
        // Если пользователя не существует -> кидаем ошибку 401
        if (userInfoDto == null)
        {
            throw new UnauthorizedException("Неверный логин или пароль!");
        }
        
        // Генерим токен доступа
        userInfoDto.AccessToken = _authService.GetAccessToken(userInfoDto);
        
        // Возвращаем информацию о пользователе
        return userInfoDto;
    }
}