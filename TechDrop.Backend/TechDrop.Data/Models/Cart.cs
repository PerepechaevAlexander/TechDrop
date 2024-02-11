using TechDrop.Data.Models.ForProduct;

namespace TechDrop.Data.Models;

/// <summary>
/// Корзина пользователя (связь Пользователь-Товар)
/// </summary>
public class Cart
{
    /// <summary>
    /// Id связи Пользователь-Товар
    /// </summary>
    public int CartId { get; set; }

    /// <summary>
    /// Количество товара в корзине
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Товар
    /// </summary>
    public Product Product { get; set; } = null!;
}