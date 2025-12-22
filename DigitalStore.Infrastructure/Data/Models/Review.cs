using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Отзывы и рейтинги товаров
/// </summary>
public partial class Review
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    /// <summary>
    /// Ссылка на заказ для подтверждения покупки
    /// </summary>
    public int? OrderId { get; set; }

    /// <summary>
    /// Оценка от 1 до 5 звезд
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Заголовок отзыва
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Текст отзыва
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Подтвержденная покупка
    /// </summary>
    public bool? IsVerifiedPurchase { get; set; }

    /// <summary>
    /// Одобрен модератором
    /// </summary>
    public bool? IsApproved { get; set; }

    /// <summary>
    /// Количество отметок &quot;полезно&quot;
    /// </summary>
    public int? HelpfulCount { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Дата редактирования
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User1 User { get; set; } = null!;
}
