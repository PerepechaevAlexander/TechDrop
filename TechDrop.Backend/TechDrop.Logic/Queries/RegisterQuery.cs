using MediatR;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Data.Models;
using TechDrop.Logic.Dtos;
using TechDrop.Logic.Exceptions;

namespace TechDrop.Logic.Queries;

/// <summary>
/// Регистрация пользователя
/// </summary>
public class RegisterQuery : IRequest<UserDto>
{
    public string Email { get; }
    public string Password { get; }

    public RegisterQuery(AuthDto authDto)
    {
        Email = authDto.Email;
        Password = authDto.Password;
    }
}

public class RegisterQueryHandler : IRequestHandler<RegisterQuery, UserDto>
{
    private readonly TechDropDbContext _dbContext;

    public RegisterQueryHandler(TechDropDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UserDto> Handle(RegisterQuery request, CancellationToken cancellationToken)
    {
        // Пытаемся найти существующего юзера с таким email-ом
        var existingUser = await _dbContext.Users
            .Where(u => u.Email.Equals(request.Email))
            .Select(u => new UserDto
            {
                UserId = u.UserId
            }).FirstOrDefaultAsync(cancellationToken);
        // Если такой юзер уже есть -> кидаем ошибку 405
        if (existingUser != null)
        {
            throw new NotAllowedException("Не удалось зарегистрироваться :(");
        }
        // Сохраняем юзера в бд
        var newUser = new User
        {
            Email = request.Email,
            Password = request.Password,
            Balance = 0
        };
        await _dbContext.Users.AddAsync(newUser, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        // Вытягиваем из бд информацию о новом юзере
        var user = await _dbContext.Users
            .Where(u => u.Email.Equals(request.Email) && u.Password.Equals(request.Password))
            .Select(u => new UserDto
            {
                UserId = u.UserId
            }).FirstOrDefaultAsync(cancellationToken);
        // Если не удалось вытянуть инфу -> кидаем ошибку 500
        if (user == null)
        {
            throw new InternalServerException("На сервере произошёл сбой. Попробуйте ещё раз.");
        }
        
        return user;
    }
}