using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Цифровой контент и лицензии
/// </summary>
public partial class DigitalContent
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    /// <summary>
    /// Путь к файлу на сервере
    /// </summary>
    public string FilePath { get; set; } = null!;

    /// <summary>
    /// Защищенная ссылка для скачивания
    /// </summary>
    public string? DownloadUrl { get; set; }

    /// <summary>
    /// Шаблон для генерации лицензионных ключей
    /// </summary>
    public string? LicenseKeyTemplate { get; set; }

    /// <summary>
    /// SHA256 хеш файла
    /// </summary>
    public string? FileHash { get; set; }

    /// <summary>
    /// Лимит скачиваний
    /// </summary>
    public int? DownloadLimit { get; set; }

    public virtual Product Product { get; set; } = null!;
}
