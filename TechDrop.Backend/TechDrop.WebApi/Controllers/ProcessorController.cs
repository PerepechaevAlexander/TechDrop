using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechDrop.Logic.Dto;
using TechDrop.Logic.Queries;

namespace TechDrop.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProcessorController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProcessorController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Получить все процессоры для страницы каталога
    /// </summary>
    /// <returns>Коллекция объектов CatalogProcessorDto</returns>
    [HttpGet("ForCatalog")]
    public async Task<IList<CatalogProcessorDto>> GetProcessors(
        string? manufacturers,
        bool? available,
        int? minCost,
        int? maxCost)
    {
        var processors = await _mediator.Send(new GetProcessorsQuery(manufacturers, available, minCost: minCost, maxCost: maxCost));
        return processors;
    }

    /// <summary>
    /// Получить все данные о процессоре по Id-шнику товара
    /// </summary>
    /// <param name="id">Id товара</param>
    /// <returns>Объект ProcessorDto</returns>
    [HttpGet("ById")]
    public async Task<ProcessorDto> GetProcessor(int id)
    {
        var processor = await _mediator.Send(new GetProcessorQuery(id));
        return processor;
    }
}