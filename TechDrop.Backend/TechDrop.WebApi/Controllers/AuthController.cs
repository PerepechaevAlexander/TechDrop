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
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _mediator.Send(new LoginQuery(loginDto));
        return Ok(user);
    }
}