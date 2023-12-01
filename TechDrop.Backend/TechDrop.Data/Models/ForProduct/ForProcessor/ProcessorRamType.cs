namespace TechDrop.Data.Models.ForProduct.ForProcessor;

/// <summary>
/// Связь Процессор-Тип памяти
/// </summary>
public class ProcessorRamType
{
    /// <summary>
    /// Id связи Процессор-Тип памяти
    /// </summary>
    public int ProcessorRamTypeId { get; set; }
    
    /// <summary>
    /// Id типа оперативной памяти
    /// </summary>
    public int RamTypeId { get; set; }
    
    /// <summary>
    /// Id характеристик процессора
    /// </summary>
    public int ProcessorId { get; set; }

    /// <summary>
    /// Тип оперативной памяти
    /// </summary>
    public RamType RamType { get; set; } = null!;

    /// <summary>
    /// Характеристики процессора
    /// </summary>
    public Processor Processor { get; set; } = null!;
}