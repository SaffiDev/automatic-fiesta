using System;
using System.Collections.Generic;
using System.Net;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// История скачивания файлов
/// </summary>
public partial class DownloadHistory
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OrderItemId { get; set; }

    /// <summary>
    /// Дата и время скачивания
    /// </summary>
    public DateTime? DownloadedAt { get; set; }

    /// <summary>
    /// IP адрес
    /// </summary>
    public IPAddress? IpAddress { get; set; }

    /// <summary>
    /// Браузер и ОС
    /// </summary>
    public string? UserAgent { get; set; }

    public virtual OrderItem OrderItem { get; set; } = null!;

    public virtual User1 User { get; set; } = null!;
}
