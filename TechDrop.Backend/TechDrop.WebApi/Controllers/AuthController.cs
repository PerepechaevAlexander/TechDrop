using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechDrop.Logic.Dtos;
using TechDrop.Logic.Queries;

namespace TechDrop.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <returns><see cref="UserDto"/></returns>
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] AuthDto authDto)
    {
        var user = await _mediator.Send(new LoginQuery(authDto));
        return Ok(user);
    }
    
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <returns><see cref="UserDto"/></returns>
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] AuthDto authDto)
    {
        var user = await _mediator.Send(new RegisterQuery(authDto));
        return Ok(user);
    }
}