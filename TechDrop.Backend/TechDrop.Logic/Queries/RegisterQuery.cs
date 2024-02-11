using MediatR;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Data.Models;
using TechDrop.Logic.Dto;
using TechDrop.Logic.Exceptions;
using TechDrop.Logic.Services;

namespace TechDrop.Logic.Queries;

/// <summary>
/// Регистрация пользователя
/// </summary>
public class RegisterQuery : IRequest<UserInfoDto>
{
    public string Email { get; }
    public string Password { get; }

    public RegisterQuery(AuthDto authDto)
    {
        Email = authDto.Email;
        Password = authDto.Password;
    }
}

public class RegisterQueryHandler : IRequestHandler<RegisterQuery, UserInfoDto>
{
    private readonly TechDropDbContext _dbContext;
    private readonly AuthService _authService;

    public RegisterQueryHandler(TechDropDbContext dbContext, AuthService authService)
    {
        _dbContext = dbContext;
        _authService = authService;
    }
    
    public async Task<UserInfoDto> Handle(RegisterQuery request, CancellationToken cancellationToken)
    {
        // Пытаемся найти существующего пользователя с таким email
        var existingUser = await _dbContext.Users
            .Where(u => u.Email.Equals(request.Email))
            .Select(u => new UserInfoDto
            {
                Email = u.Email
            }).FirstOrDefaultAsync(cancellationToken);
        
        // Если такой пользователь уже есть -> кидаем ошибку 405
        if (existingUser != null)
        {
            throw new NotAllowedException("Не удалось зарегистрироваться :(");
        }
        
        // Сохраняем пользователя в бд
        var newUser = new User
        {
            Email = request.Email,
            Password = request.Password,
            Balance = 0
        };
        await _dbContext.Users.AddAsync(newUser, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        // Вытягиваем из бд информацию о созданном пользователе
        var userInfoDto = await _dbContext.Users
            .Where(u => u.Email.Equals(request.Email) && u.Password.Equals(request.Password))
            .Select(u => new UserInfoDto
            {
                Email = u.Email
            }).FirstOrDefaultAsync(cancellationToken);
        
        // Если не удалось вытянуть инфу -> кидаем ошибку 500
        if (userInfoDto == null)
        {
            throw new InternalServerException("Ошибка регистрации! Попробуйте ещё раз");
        }
        
        // Генерим токен доступа
        userInfoDto.AccessToken = _authService.GetAccessToken(userInfoDto);
        
        return userInfoDto;
    }
}