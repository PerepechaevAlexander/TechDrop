namespace TechDrop.Data.Models;

/// <summary>
/// Статус(состояние) заказа
/// </summary>
public class OrderStatus
{
    /// <summary>
    /// Id статуса заказа
    /// </summary>
    public int OrderStatusId { get; set; }

    /// <summary>
    /// Наименование статуса заказа
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Список заказов с таким статусом
    /// </summary>
    public List<Order> Orders { get; set; } = new();
}