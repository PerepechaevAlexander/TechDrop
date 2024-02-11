using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Data.Models.ForProduct;

namespace TechDrop.Logic.Services;

/// <summary>
/// Сервис для работы с объектом <see cref="Product"/>,
/// содержит get\set\check для <see cref="Product"/> и его свойств.
/// </summary>
public class ProductService
{
    private readonly TechDropDbContext _dbContext;

    public ProductService(TechDropDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Проверить, существует ли товар в бд, по Id.
    /// </summary>
    /// <param name="productId">Id товара;</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>True - товар найден в бд; False - товар НЕ найден в бд.</returns>
    public async Task<bool> CheckProductById(int productId, CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .Where(p => p.ProductId == productId)
            .AnyAsync(cancellationToken);
    }
}