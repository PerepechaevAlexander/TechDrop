using TechDrop.Data.Models.ForProduct;

namespace TechDrop.Data.Models;

/// <summary>
/// Товар, входящий в состав заказа (связь Заказ-Товар)
/// </summary>
public class OrderProduct
{
    /// <summary>
    /// Id связи Заказ-Товар
    /// </summary>
    public int OrderProductId { get; set; }
    
    /// <summary>
    /// Количество товара в заказе
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Заказ
    /// </summary>
    public Order Order { get; set; } = null!;

    /// <summary>
    /// Товар
    /// </summary>
    public Product Product { get; set; } = null!;
}