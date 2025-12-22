using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Избранные товары пользователей
/// </summary>
public partial class UserWishlist
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    /// <summary>
    /// Дата добавления в избранное
    /// </summary>
    public DateTime? AddedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User1 User { get; set; } = null!;
}
