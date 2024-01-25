namespace TechDrop.Logic.Dto;

/// <summary>
/// Процессор для отображения в каталоге
/// </summary>
public class CatalogProcessorDto
{
     /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Стоимость товара
    /// </summary>
    public double Cost { get; set; }
    
    /// <summary>
    /// Количество товара на складе
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Скидка на товар
    /// </summary>
    public double Discount { get; set; }

    /// <summary>
    /// ВСЕ изображения товара
    /// </summary>
    public IList<ProductPictureDto> Pictures { get; set; } = null!;

    /// <summary>
    /// Производитель
    /// </summary>
    public string Manufacturer { get; set; } = null!;
    
    /// <summary>
    /// Id характеристик процессора
    /// </summary>
    public int ProcessorId { get; set; }
    
    /// <summary>
    /// Модель
    /// </summary>
    public string Model { get; set; } = null!;
    
    /// <summary>
    /// Общее количество ядер
    /// </summary>
    public int Cores { get; set; }
    
    /// <summary>
    /// Базовая частота процессора
    /// </summary>
    public double BaseFrequency { get; set; }
    
    /// <summary>
    /// Максимальная частота процессора
    /// </summary>
    public double? MaxFrequency { get; set; }

    /// <summary>
    /// Техпроцесс 
    /// </summary>
    public int TechProcess { get; set; }

    /// <summary>
    /// Модель интегрированного графического ядра
    /// </summary>
    public string? GraphCoreModel { get; set; }
    
    /// <summary>
    /// Тепловыделение 
    /// </summary>
    public int Tdp { get; set; }
}