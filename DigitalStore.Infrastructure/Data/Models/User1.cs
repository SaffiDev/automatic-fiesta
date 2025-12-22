using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Зарегистрированные пользователи магазина
/// </summary>
public partial class User1
{
    public int Id { get; set; }

    /// <summary>
    /// Уникальный логин
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Email для входа
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// Имя
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Телефон
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<DownloadHistory> DownloadHistories { get; set; } = new List<DownloadHistory>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserPaymentMethod> UserPaymentMethods { get; set; } = new List<UserPaymentMethod>();

    public virtual ICollection<UserWishlist> UserWishlists { get; set; } = new List<UserWishlist>();
}
