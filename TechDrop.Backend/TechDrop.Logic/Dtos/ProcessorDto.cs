namespace TechDrop.Logic.Dtos;

/// <summary>
/// Процессор для отображения на странице товара
/// </summary>
public class ProcessorDto
{
    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Описание товара
    /// </summary>
    public string Description { get; set; } = null!;
    
    /// <summary>
    /// Стоимость товара
    /// </summary>
    public double Cost { get; set; }
    
    /// <summary>
    /// Количество товара на складе
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Скидка на товар
    /// </summary>
    public double Discount { get; set; }

    /// <summary>
    /// ВСЕ изображения товара
    /// </summary>
    public IList<ProductPictureDto> Pictures { get; set; } = null!;

    /// <summary>
    /// Производитель
    /// </summary>
    public string Manufacturer { get; set; } = null!;

    /// <summary>
    /// Id характеристик процессора
    /// </summary>
    public int ProcessorId { get; set; }
    
    /// <summary>
    /// Модель
    /// </summary>
    public string Model { get; set; } = null!;
    
    /// <summary>
    /// Сокет
    /// </summary>
    public string Socket { get; set; } = null!;
    
    /// <summary>
    /// Год релиза
    /// </summary>
    public int Year { get; set; }
    
    /// <summary>
    /// Наличие системы охлаждения в комплекте
    /// </summary>
    public bool CoolingSystem { get; set; }
    
    /// <summary>
    /// Общее количество ядер
    /// </summary>
    public int Cores { get; set; }

    /// <summary>
    /// Максимальное число потоков
    /// </summary>
    public int Threads { get; set; }

    /// <summary>
    /// Количество производительных ядер
    /// </summary>
    public int PerformanceCores { get; set; }

    /// <summary>
    /// Количество энергоэффективных ядер
    /// </summary>
    public int EnergyCores { get; set; }
    
    /// <summary>
    /// Объем кэша L2
    /// </summary>
    public double L2 { get; set; }
    
    /// <summary>
    /// Объем кэша L3
    /// </summary>
    public double L3 { get; set; }

    /// <summary>
    /// Техпроцесс 
    /// </summary>
    public int TechProcess { get; set; }
    
    /// <summary>
    /// Базовая частота процессора
    /// </summary>
    public double BaseFrequency { get; set; }
    
    /// <summary>
    /// Максимальная частота процессора
    /// </summary>
    public double? MaxFrequency { get; set; }
    
    /// <summary>
    /// Базовая частота энергоэффективных ядер
    /// </summary>
    public double? BaseFrequencyEnergyCores { get; set; }
    
    /// <summary>
    /// Максимальная частота энергоэффективных ядер
    /// </summary>
    public double? MaxFrequencyEnergyCores { get; set; }
    
    /// <summary>
    /// Свободный множитель
    /// </summary>
    public bool FreeMultiplier { get; set; }

    /// <summary>
    /// Типы поддерживаемой оперативной памяти
    /// </summary>
    public List<string> RamTypes { get; set; } = null!;

    /// <summary>
    /// Максимально поддерживаемый объем оперативной памяти
    /// </summary>
    public int RamCapacity { get; set; }

    /// <summary>
    /// Количество каналов оперативной памяти
    /// </summary>
    public int RamChannels { get; set; }
    
    /// <summary>
    /// Максимальная частота оперативной памяти
    /// </summary>
    public int RamMaxFrequency { get; set; }
    
    /// <summary>
    /// Тепловыделение 
    /// </summary>
    public int Tdp { get; set; }
    
    /// <summary>
    /// Максимальная температура процессора
    /// </summary>
    public int MaxTemp { get; set; }

    /// <summary>
    /// Наличие графического ядра
    /// </summary>
    public bool GraphCoreAvailable { get; set; }

    /// <summary>
    /// Интегрированное графическое ядро
    /// </summary>
    public GraphCoreDto? GraphCore { get; set; }

    /// <summary>
    /// Контроллер PCI-Express
    /// </summary>
    public string PciExpress { get; set; } = null!;

    /// <summary>
    /// Число линий PCI-Express
    /// </summary>
    public int PciExpressLines { get; set; }
}