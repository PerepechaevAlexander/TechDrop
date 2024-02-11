namespace TechDrop.Data.Models.ForProduct;

/// <summary>
/// Производитель
/// </summary>
public class Manufacturer
{
    /// <summary>
    /// Id производителя
    /// </summary>
    public int ManufacturerId { get; set; }

    /// <summary>
    /// Наименование производителя
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Товары, относящиеся к производителю
    /// </summary>
    public List<Product> Products { get; set; } = new();
}