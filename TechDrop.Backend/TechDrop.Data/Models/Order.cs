namespace TechDrop.Data.Models;

/// <summary>
/// Заказ
/// </summary>
public class Order
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Стоимость заказа
    /// </summary>
    public double Cost { get; set; }

    /// <summary>
    /// Id пользователя, сделавшего заказ
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Id статуса заказ
    /// </summary>
    public int OrderStatusId { get; set; }

    /// <summary>
    /// Пользователь, сделавший заказ
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus OrderStatus { get; set; } = null!;

    /// <summary>
    /// Список товаров в заказе (связей Заказ-Товар)
    /// </summary>
    public List<OrderProduct> OrderProducts { get; set; } = new();
}