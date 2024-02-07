using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechDrop.Logic.Commands;

namespace TechDrop.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Добавить товар в корзину.
    /// </summary>
    /// <returns><see cref="IActionResult"/></returns>
    [HttpPost(nameof(AddProductById))]
    public async Task<IActionResult> AddProductById([FromBody] int productId)
    {
        await _mediator.Send(new AddToCartCommand(productId));
        return Ok();
    }
}