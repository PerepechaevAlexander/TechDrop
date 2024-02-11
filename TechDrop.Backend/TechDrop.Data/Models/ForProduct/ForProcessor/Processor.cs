namespace TechDrop.Data.Models.ForProduct.ForProcessor;

/// <summary>
/// Характеристики процессора
/// </summary>
public class Processor
{
    /// <summary>
    /// Id характеристик процессора
    /// </summary>
    public int ProcessorId { get; set; }

    /// <summary>
    /// Модель
    /// </summary>
    public string Model { get; set; } = null!;

    /// <summary>
    /// Id сокета
    /// </summary>
    public int SocketId { get; set; }

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
    /// Id интегрированного графического ядра
    /// </summary>
    public int? GraphCoreId { get; set; }

    /// <summary>
    /// Id контроллера PCI-Express
    /// </summary>
    public int PciExpressId { get; set; }

    /// <summary>
    /// Число линий PCI-Express
    /// </summary>
    public int PciExpressLines { get; set; }
    
    /// <summary>
    /// Товар
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Тип оперативной памяти, поддерживаемой процессором (связка Процессор-Тип памяти)
    /// </summary>
    public List<ProcessorRamType> ProcessorRamTypes { get; set; } = null!;

    /// <summary>
    /// Контроллер PCI-Express
    /// </summary>
    public PciExpress PciExpress { get; set; } = null!;

    /// <summary>
    /// Сокет
    /// </summary>
    public Socket Socket { get; set; } = null!;

    /// <summary>
    /// Интегрированное графическое ядро
    /// </summary>
    public GraphCore? GraphCore { get; set; } = null!;
}