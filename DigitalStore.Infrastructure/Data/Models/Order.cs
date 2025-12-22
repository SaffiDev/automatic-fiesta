using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Заказы покупателей
/// </summary>
public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    /// <summary>
    /// Уникальный номер заказа
    /// </summary>
    public Guid OrderNumber { get; set; }

    /// <summary>
    /// Сумма до скидки
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Размер скидки
    /// </summary>
    public decimal? DiscountAmount { get; set; }

    /// <summary>
    /// Итоговая сумма к оплате
    /// </summary>
    public decimal FinalAmount { get; set; }

    /// <summary>
    /// Статус: pending, paid, completed, cancelled, refunded
    /// </summary>
    public string Status { get; set; } = null!;

    public int? PaymentMethodId { get; set; }

    /// <summary>
    /// Использованный промокод
    /// </summary>
    public string? DiscountCode { get; set; }

    /// <summary>
    /// Дата создания заказа
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Дата оплаты
    /// </summary>
    public DateTime? PaidAt { get; set; }

    /// <summary>
    /// Дата завершения
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual UserPaymentMethod? PaymentMethod { get; set; }

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual User1 User { get; set; } = null!;
}
