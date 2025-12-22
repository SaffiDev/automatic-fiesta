using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// История платежных транзакций
/// </summary>
public partial class PaymentTransaction
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    /// <summary>
    /// ID транзакции в платежной системе
    /// </summary>
    public string TransactionId { get; set; } = null!;

    /// <summary>
    /// Платежная система: yookassa, stripe, paypal
    /// </summary>
    public string PaymentSystem { get; set; } = null!;

    /// <summary>
    /// Сумма платежа
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; set; } = null!;

    /// <summary>
    /// Статус: pending, success, failed
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Дата транзакции
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
