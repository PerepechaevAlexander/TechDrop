namespace TechDrop.Data.Models.ForProduct.ForProcessor;

/// <summary>
/// Тип оперативной памяти
/// </summary>
public class RamType
{
    /// <summary>
    /// Id типа оперативной памяти
    /// </summary>
    public int RamTypeId { get; set; }

    /// <summary>
    /// Наименование типа оперативной памяти
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Список процессоров, поддерживающих этот тип памяти (связей Процессор-Тип памяти)
    /// </summary>
    public List<ProcessorRamType> ProcessorsRamTypes { get; set; } = new();
}