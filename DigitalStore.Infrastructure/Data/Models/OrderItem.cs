using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Позиции в заказе (что именно купили)
/// </summary>
public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    /// <summary>
    /// Название товара на момент покупки
    /// </summary>
    public string ProductName { get; set; } = null!;

    /// <summary>
    /// Количество
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Цена за единицу
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Общая стоимость
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Лицензионный ключ (для ПО)
    /// </summary>
    public string? LicenseKey { get; set; }

    /// <summary>
    /// Токен для безопасного скачивания
    /// </summary>
    public Guid? DownloadToken { get; set; }

    /// <summary>
    /// Срок действия ссылки для скачивания
    /// </summary>
    public DateTime? DownloadExpiresAt { get; set; }

    /// <summary>
    /// Осталось скачиваний
    /// </summary>
    public int? DownloadsRemaining { get; set; }

    public virtual ICollection<DownloadHistory> DownloadHistories { get; set; } = new List<DownloadHistory>();

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
