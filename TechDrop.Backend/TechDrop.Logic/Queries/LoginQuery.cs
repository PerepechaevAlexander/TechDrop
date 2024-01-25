using MediatR;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Logic.Dtos;
using TechDrop.Logic.Exceptions;

namespace TechDrop.Logic.Queries;

/// <summary>
/// Авторизация пользователя
/// </summary>
public class LoginQuery : IRequest<UserDto>
{
    public string Email { get; }
    public string Password { get; }

    public LoginQuery(AuthDto authDto)
    {
        Email = authDto.Email;
        Password = authDto.Password;
    }
}

public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
{
    private readonly TechDropDbContext _dbContext;

    public LoginQueryHandler(TechDropDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        // Пытаемся найти пользователя
        var user = await _dbContext.Users
            .Where(u => u.Email.Equals(request.Email) && u.Password.Equals(request.Password))
            .Select(u => new UserDto
            {
                UserId = u.UserId
            }).FirstOrDefaultAsync(cancellationToken);
        // Если его нет -> кидаем ошибку 401
        if (user == null)
        {
            throw new UnauthorizedException("Неверный логин или пароль!");
        }
        
        return user;
    }
}