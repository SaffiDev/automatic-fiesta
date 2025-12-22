using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Категории товаров: музыка, книги, ПО, игры
/// </summary>
public partial class Category
{
    public int Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Описание категории
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Родительская категория (для вложенности)
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// URL иконки категории
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Активна ли категория
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Порядок сортировки
    /// </summary>
    public int? SortOrder { get; set; }

    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    public virtual Category? Parent { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
