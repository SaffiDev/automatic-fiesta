using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Сохраненные способы оплаты пользователей
/// </summary>
public partial class UserPaymentMethod
{
    public int Id { get; set; }

    public int UserId { get; set; }

    /// <summary>
    /// Тип карты: visa, mastercard, mir
    /// </summary>
    public string CardType { get; set; } = null!;

    /// <summary>
    /// Последние 4 цифры карты
    /// </summary>
    public string LastFour { get; set; } = null!;

    /// <summary>
    /// Срок действия MM/YY
    /// </summary>
    public string ExpiryDate { get; set; } = null!;

    /// <summary>
    /// Карта по умолчанию
    /// </summary>
    public bool? IsDefault { get; set; }

    /// <summary>
    /// Дата добавления
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User1 User { get; set; } = null!;
}
