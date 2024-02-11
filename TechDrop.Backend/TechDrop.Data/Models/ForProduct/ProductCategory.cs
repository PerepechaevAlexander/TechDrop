namespace TechDrop.Data.Models.ForProduct;

/// <summary>
/// Категория товара
/// </summary>
public class ProductCategory
{
    /// <summary>
    /// Id категории товара
    /// </summary>
    public int ProductCategoryId { get; set; }

    /// <summary>
    /// Наименование категории товара
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Изображение
    /// </summary>
    public byte[] Picture { get; set; } = null!;

    /// <summary>
    /// Товары, относящиеся к категории
    /// </summary>
    public List<Product> Products { get; set; } = new();
}