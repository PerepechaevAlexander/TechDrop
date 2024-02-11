using MediatR;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Logic.Dto;

namespace TechDrop.Logic.Queries;

/// <summary>
/// Получить все процессоры для страницы каталога
/// </summary>
public class GetProcessorsQuery : IRequest<IList<CatalogProcessorDto>>
{
    /// <summary>
    /// TEMP Производитель
    /// </summary>
    public string[]? Manufacturers { get; set; }

    /// <summary>
    /// TEMP Доступность.
    /// True - только доступные для заказа, False - все, находящиеся в бд.
    /// </summary>
    public bool? Available { get; set; }
    
    /// <summary>
    /// TEMP Минимальная стоимость
    /// </summary>
    public int? MinCost { get; set; }
    
    /// <summary>
    /// TEMP Максимальная стоимость
    /// </summary>
    public int? MaxCost { get; set; }

    public GetProcessorsQuery(string? manufacturers, bool? available, int? minCost = null, int? maxCost = null)
    {
        Manufacturers = manufacturers?.Split('-');
        Available = available;
        MinCost = minCost;
        MaxCost = maxCost;
    }
}

public class GetProcessorsQueryHandler : IRequestHandler<GetProcessorsQuery, IList<CatalogProcessorDto>>
{
    private readonly TechDropDbContext _dbContext;

    public GetProcessorsQueryHandler(TechDropDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<CatalogProcessorDto>> Handle(GetProcessorsQuery request, CancellationToken cancellationToken)
    {
        // Получаем лист CatalogProcessorDto без картинок
        var processors = await _dbContext.Products
            .Where(p => p.ProcessorId != null)
            .Select(product => new CatalogProcessorDto
            {
                ProductId = product.ProductId,
                Cost = product.Cost,
                Quantity = product.Quantity,
                Discount = product.Discount,
                Manufacturer = product.Manufacturer.Name,
                ProcessorId = (int)product.ProcessorId!,
                Model = product.Processor!.Model,
                Cores = product.Processor!.Cores,
                BaseFrequency = product.Processor!.BaseFrequency,
                MaxFrequency = product.Processor!.MaxFrequency,
                TechProcess = product.Processor!.TechProcess,
                Tdp = product.Processor!.Tdp,
                GraphCoreModel = product.Processor!.GraphCore.Model
            })
            .OrderBy(dto => dto.Cost)
            .ToListAsync(cancellationToken);

        // Добавляем картинки
        foreach (var processor in processors)
        {
            var pictures = await _dbContext.ProductPictures
                .Where(pp => pp.ProductId == processor.ProductId)
                .Select(pp => new ProductPictureDto
                {
                    PictureId = pp.PictureId,
                    Picture = pp.Picture.Resource
                }).ToListAsync(cancellationToken);
            processor.Pictures = pictures;
        }

        // TEMP Фильтрация по минимальной стоимости
        if (request.MinCost != null)
        {
            processors = processors.Where(p => p.Cost >= request.MinCost)
                .ToList();
        }

        // TEMP Фильтрация по максимальной стоимости
        if (request.MaxCost != null)
        {
            processors = processors.Where(p => p.Cost <= request.MaxCost)
                .ToList();
        }

        // TEMP Фильтрация по производителю
        if (request.Manufacturers != null && request.Manufacturers.Any())
        {
            processors = processors.Where(p => request.Manufacturers.Contains(p.Manufacturer))
                .ToList();
        }

        // TEMP Фильтрация по доступности на складе
        if (request.Available != null && (bool)request.Available)
        {
            processors = processors.Where(p => p.Quantity > 0).ToList();
        }

        return processors;
    }
}