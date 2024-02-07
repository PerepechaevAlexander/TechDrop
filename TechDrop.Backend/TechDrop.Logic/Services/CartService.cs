using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Data.Models;

namespace TechDrop.Logic.Services;

/// <summary>
/// Сервис для работы с объектом <see cref="Cart"/>,
/// содержит get\set\check для <see cref="Cart"/> и его свойств.
/// </summary>
public class CartService
{
    private readonly TechDropDbContext _dbContext;

    public CartService(TechDropDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Получить запись в корзине.
    /// </summary>
    /// <param name="productId">Id товара;</param>
    /// <param name="userId">Id пользователя;</param>
    /// <param name="cancellationToken">токен отмены.</param>
    public async Task<Cart?> GetCart(int productId, int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Carts
            .Where(c => c.UserId == userId && c.ProductId == productId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}