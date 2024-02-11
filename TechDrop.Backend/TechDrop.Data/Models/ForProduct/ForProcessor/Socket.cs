namespace TechDrop.Data.Models.ForProduct.ForProcessor;

/// <summary>
/// Сокет процессора
/// </summary>
public class Socket
{
    /// <summary>
    /// Id сокета
    /// </summary>
    public int SocketId { get; set; }

    /// <summary>
    /// Наименование сокета
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Список процессоров, имеющих этот сокет
    /// </summary>
    public List<Processor> Processors { get; set; } = new();
}