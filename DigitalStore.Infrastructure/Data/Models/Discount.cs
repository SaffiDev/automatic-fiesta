using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Скидки и промокоды
/// </summary>
public partial class Discount
{
    public int Id { get; set; }

    /// <summary>
    /// Промокод
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Описание акции
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Тип скидки: percentage (процент), fixed (фиксированная)
    /// </summary>
    public string DiscountType { get; set; } = null!;

    /// <summary>
    /// Размер скидки
    /// </summary>
    public decimal DiscountValue { get; set; }

    /// <summary>
    /// Минимальная сумма покупки
    /// </summary>
    public decimal? MinPurchaseAmount { get; set; }

    /// <summary>
    /// Максимальное количество использований
    /// </summary>
    public int? MaxUses { get; set; }

    /// <summary>
    /// Текущее количество использований
    /// </summary>
    public int? CurrentUses { get; set; }

    /// <summary>
    /// Действует с
    /// </summary>
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// Действует до
    /// </summary>
    public DateTime? ValidUntil { get; set; }

    /// <summary>
    /// Активна ли скидка
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
