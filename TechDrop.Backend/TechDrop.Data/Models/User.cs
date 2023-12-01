namespace TechDrop.Data.Models;

/// <summary>
/// Пользователь
/// </summary>
public class User
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; } = null!;
    
    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Баланс кошелька пользователя
    /// </summary>
    public double Balance { get; set; } = 0;

    /// <summary>
    /// Список товаров в корзине пользователя
    /// </summary>
    public List<Cart> Carts { get; set; } = new();

    /// <summary>
    /// Список заказов пользователя
    /// </summary>
    public List<Order> Orders { get; set; } = new();
}