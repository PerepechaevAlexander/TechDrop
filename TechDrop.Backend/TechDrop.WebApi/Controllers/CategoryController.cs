using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechDrop.Logic.Dto;
using TechDrop.Logic.Queries;

namespace TechDrop.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить все существующие категории товаров
    /// </summary>
    /// <returns>Коллекция CategoryDto</returns>
    [HttpGet]
    public async Task<IList<CategoryDto>> GetCategories()
    {
        var categories = await _mediator.Send(new GetCategoriesQuery());
        return categories;
    }
}