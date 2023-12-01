namespace TechDrop.Data.Models.ForProduct;

/// <summary>
/// Связь Товар-Изображение
/// </summary>
public class ProductPicture
{
    /// <summary>
    /// Id связи Товар-Изображение
    /// </summary>
    public int ProductPictureId { get; set; }

    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Id изображения
    /// </summary>
    public int PictureId { get; set; }

    /// <summary>
    /// Товар
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Изображение
    /// </summary>
    public Picture Picture { get; set; } = null!;
}