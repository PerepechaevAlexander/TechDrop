using MediatR;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Logic.Dtos;

namespace TechDrop.Logic.Queries;

/// <summary>
/// Получить все данные о процессоре по Id-шнику товара
/// </summary>
public class GetProcessorQuery : IRequest<ProcessorDto>
{
    public int Id { get; }

    public GetProcessorQuery(int id)
    {
        Id = id;
    }
}

public class GetProcessorQueryHandler : IRequestHandler<GetProcessorQuery, ProcessorDto>
{
    private readonly TechDropDbContext _dbContext;

    public GetProcessorQueryHandler(TechDropDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ProcessorDto> Handle(GetProcessorQuery request, CancellationToken cancellationToken)
    {
        // Добавляем поля из таблицы "Товар"
        var processor = await _dbContext.Products
            .Where(product => product.ProductId == request.Id)
            .Select(p => new ProcessorDto
            {
                ProductId = p.ProductId,
                Description = p.Description,
                Cost = p.Cost,
                Quantity = p.Quantity,
                Discount = p.Discount,
                Manufacturer = p.Manufacturer.Name,
                ProcessorId = (int)p.ProcessorId!,
                Model = p.Processor!.Model,
                Socket = p.Processor.Socket.Name,
                Year = p.Processor.Year,
                CoolingSystem = p.Processor.CoolingSystem,
                Cores = p.Processor.Cores,
                Threads = p.Processor.Threads,
                PerformanceCores = p.Processor.PerformanceCores,
                EnergyCores = p.Processor.EnergyCores,
                L2 = p.Processor.L2,
                L3 = p.Processor.L3,
                TechProcess = p.Processor.TechProcess,
                BaseFrequency = p.Processor.BaseFrequency,
                MaxFrequency = p.Processor.MaxFrequency,
                BaseFrequencyEnergyCores = p.Processor.BaseFrequencyEnergyCores,
                MaxFrequencyEnergyCores = p.Processor.MaxFrequencyEnergyCores,
                FreeMultiplier = p.Processor.FreeMultiplier,
                RamTypes = p.Processor.ProcessorRamTypes.Select(prt => prt.RamType.Name).ToList(),
                RamCapacity = p.Processor.RamCapacity,
                RamChannels = p.Processor.RamChannels,
                RamMaxFrequency = p.Processor.RamMaxFrequency,
                Tdp = p.Processor.Tdp,
                MaxTemp = p.Processor.MaxTemp,
                PciExpress = p.Processor.PciExpress.Name,
                PciExpressLines = p.Processor.PciExpressLines
            }).FirstOrDefaultAsync(cancellationToken);
        
        // Добавляем изображения
        var pictures = await _dbContext.ProductPictures
            .Where(pp => pp.ProductId == processor.ProductId)
            .Select(pp => new ProductPictureDto
            {
                PictureId = pp.PictureId,
                Picture = pp.Picture.Resource
            }).ToListAsync(cancellationToken);
        processor.Pictures = pictures;
        
        // Добавляем графическое ядро, если оно есть
        var graphCore = await _dbContext.Processors
            .Where(proc => proc.ProcessorId == processor.ProcessorId)
            .Select(proc => proc.GraphCore).FirstOrDefaultAsync(cancellationToken);
        if (graphCore != null)
        {
            processor.GraphCoreAvailable = true;
            processor.GraphCore = new GraphCoreDto
            {
                Model = graphCore.Model,
                MaxFrequency = graphCore.MaxFrequency,
                ExecutiveBlocks = graphCore.ExecutiveBlocks,
                ShadingUnits = graphCore.ShadingUnits
            };
        }
        
        return processor;
    }
}