namespace TechDrop.Logic.Dtos;

/// <summary>
/// Изображение товара
/// </summary>
public class ProductPictureDto
{
    /// <summary>
    /// Id изображения
    /// </summary>
    public int PictureId { get; set; }

    /// <summary>
    /// Изображение
    /// </summary>
    public byte[] Picture { get; set; } = null!;
}