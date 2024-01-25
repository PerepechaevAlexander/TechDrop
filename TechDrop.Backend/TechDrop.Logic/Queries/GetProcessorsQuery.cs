using MediatR;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Logic.Dto;

namespace TechDrop.Logic.Queries;

/// <summary>
/// Получить все процессоры для страницы каталога
/// </summary>
public class GetProcessorsQuery : IRequest<IList<CatalogProcessorDto>> { }

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
            }).ToListAsync(cancellationToken);

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

        return processors;
    }
}