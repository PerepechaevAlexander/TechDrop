using MediatR;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Logic.Dto;

namespace TechDrop.Logic.Queries;

/// <summary>
/// Проверка пользователя в AuthMiddleware
/// </summary>
public class CheckUserQuery : IRequest<UserDto?>
{
    public string Email { get; }
    public string Password { get; }

    public CheckUserQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

public class CheckUserQueryHandler : IRequestHandler<CheckUserQuery, UserDto?>
{
    private readonly TechDropDbContext _dbContext;

    public CheckUserQueryHandler(TechDropDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UserDto?> Handle(CheckUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(u => u.Email.Equals(request.Email) && u.Password.Equals(request.Password))
            .Select(u => new UserDto
            {
                UserId = u.UserId
            }).FirstOrDefaultAsync(cancellationToken);
        return user;
    }
}