using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechDrop.Logic.Commands;
using TechDrop.Logic.Queries;

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
    /// Получить кол-во каждого товара в корзине.
    /// </summary>
    [HttpGet(nameof(GetQuantityOfProducts))]
    public async Task<IActionResult> GetQuantityOfProducts()
    {
        var productQuantityDtos = await _mediator.Send(new GetQuantityOfProductsInCartQuery());
        return Ok(productQuantityDtos);
    }
    
    /// <summary>
    /// Добавить товар в корзину.
    /// </summary>
    [HttpPost(nameof(AddProductById))]
    public async Task<IActionResult> AddProductById([FromBody] int productId)
    {
        await _mediator.Send(new AddToCartCommand(productId));
        return Ok();
    }
}