using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Изображения товаров
/// </summary>
public partial class ProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    /// <summary>
    /// URL изображения
    /// </summary>
    public string ImageUrl { get; set; } = null!;

    /// <summary>
    /// Главное изображение товара
    /// </summary>
    public bool? IsPrimary { get; set; }

    /// <summary>
    /// Порядок отображения
    /// </summary>
    public int? SortOrder { get; set; }

    public virtual Product Product { get; set; } = null!;
}
