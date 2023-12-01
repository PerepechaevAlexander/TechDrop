namespace TechDrop.Data.Models.ForProduct;

/// <summary>
/// Изображение товара
/// </summary>
public class Picture
{
    /// <summary>
    /// Id изображения
    /// </summary>
    public int PictureId { get; set; }

    /// <summary>
    /// Ресурс изображения
    /// </summary>
    public byte[] Resource { get; set; } = null!;

    /// <summary>
    /// Связь Товар-Изображение
    /// </summary>
    public ProductPicture ProductPictures { get; set; } = null!;
}