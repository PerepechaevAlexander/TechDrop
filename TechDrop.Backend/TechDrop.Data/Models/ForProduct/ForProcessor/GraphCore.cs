namespace TechDrop.Data.Models.ForProduct.ForProcessor;

/// <summary>
/// Интегрированное графическое ядро
/// </summary>
public class GraphCore
{
    /// <summary>
    /// Id интегрированного графического ядра
    /// </summary>
    public int GraphCoreId { get; set; }

    /// <summary>
    /// Наименование модели графического ядра
    /// </summary>
    public string Model { get; set; } = null!;
    
    /// <summary>
    /// Максимальная частота графического ядра
    /// </summary>
    public int MaxFrequency { get; set; }
    
    /// <summary>
    /// Количество исполнительных блоков
    /// </summary>
    public int ExecutiveBlocks { get; set; }
    
    /// <summary>
    /// Количество потоковых процессоров
    /// </summary>
    public int ShadingUnits { get; set; }

    /// <summary>
    /// Список процессоров, имеющих это графическое ядро
    /// </summary>
    public List<Processor> Processors { get; set; } = new();
}