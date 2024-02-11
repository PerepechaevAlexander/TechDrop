using TechDrop.Data.Models.ForProduct.ForProcessor;

namespace TechDrop.Data.Models.ForProduct;

/// <summary>
/// Товар
/// </summary>
public class Product
{
    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Описание товара
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Стоимость товара
    /// </summary>
    public double Cost { get; set; }

    /// <summary>
    /// Количество товара на складе
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Скидка на товар, default = 0
    /// </summary>
    public double Discount { get; set; } = 0;

    /// <summary>
    /// Id категории товара
    /// </summary>
    public int ProductCategoryId { get; set; }

    /// <summary>
    /// Id производителя
    /// </summary>
    public int ManufacturerId { get; set; }

    /// <summary>
    /// Id характеристик процессора
    /// </summary>
    public int? ProcessorId { get; set; }

    /// <summary>
    /// Список записей в корзине, к которым относится товар
    /// </summary>
    public List<Cart> Carts { get; set; } = new();

    /// <summary>
    /// Список заказов, к которым относится товар
    /// </summary>
    public List<OrderProduct> OrderProducts { get; set; } = new();

    /// <summary>
    /// Характеристики процессора
    /// </summary>
    public Processor? Processor { get; set; }

    /// <summary>
    /// Категория товара
    /// </summary>
    public ProductCategory ProductCategory { get; set; } = null!;

    /// <summary>
    /// Производитель
    /// </summary>
    public Manufacturer Manufacturer { get; set; } = null!;

    /// <summary>
    /// Список изображений товара (связей Товар-Изображение)
    /// </summary>
    public List<ProductPicture> ProductPictures { get; set; } = new();
}