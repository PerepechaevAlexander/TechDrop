namespace TechDrop.Data.Models.ForProduct.ForProcessor;

/// <summary>
/// Версия контроллера PCI-Express
/// </summary>
public class PciExpress
{
    /// <summary>
    /// Id версии контроллера PCI-Express
    /// </summary>
    public int PciExpressId { get; set; }

    /// <summary>
    /// Наименование версии контроллера PCI-Express
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Список процессоров, имеющих эту версию контроллера PCI-Express
    /// </summary>
    public List<Processor> Processors { get; set; } = new();
}