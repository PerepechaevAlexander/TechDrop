namespace TechDrop.Logic.Dto;

/// <summary>
/// Кол-во товара
/// </summary>
public class ProductQuantityDto
{
    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Количество товара
    /// </summary>
    public int Quantity { get; set; }
}