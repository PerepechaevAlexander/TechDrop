namespace TechDrop.Logic.Dtos;

public class GraphCoreDto
{
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
}