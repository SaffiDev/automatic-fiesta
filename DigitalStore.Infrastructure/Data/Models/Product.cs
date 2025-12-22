using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Цифровые товары магазина
/// </summary>
public partial class Product
{
    public int Id { get; set; }

    /// <summary>
    /// Название товара
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Описание товара
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Цена в рублях
    /// </summary>
    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    /// <summary>
    /// Тип: music, book, software, game
    /// </summary>
    public string ProductType { get; set; } = null!;

    /// <summary>
    /// Размер файла в МБ
    /// </summary>
    public decimal? FileSizeMb { get; set; }

    /// <summary>
    /// Версия (для ПО)
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Автор или исполнитель
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Издатель
    /// </summary>
    public string? Publisher { get; set; }

    /// <summary>
    /// Дата выхода
    /// </summary>
    public DateOnly? ReleaseDate { get; set; }

    /// <summary>
    /// Язык товара
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Доступен ли товар
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Количество лицензий
    /// </summary>
    public int? StockQuantity { get; set; }

    /// <summary>
    /// Дополнительные характеристики (битрейт, формат, и т.д.)
    /// </summary>
    public string? Metadata { get; set; }

    /// <summary>
    /// Средний рейтинг
    /// </summary>
    public decimal? AvgRating { get; set; }

    /// <summary>
    /// Количество отзывов
    /// </summary>
    public int? ReviewCount { get; set; }

    /// <summary>
    /// Количество продаж
    /// </summary>
    public int? SalesCount { get; set; }

    /// <summary>
    /// Дата добавления
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category Category { get; set; } = null!;

    public virtual DigitalContent? DigitalContent { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserWishlist> UserWishlists { get; set; } = new List<UserWishlist>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
