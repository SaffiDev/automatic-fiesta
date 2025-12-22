using System;
using System.Collections.Generic;

namespace DigitalStore.Infrastructure.Data.Models;

/// <summary>
/// Администраторы системы
/// </summary>
public partial class Admin
{
    public int Id { get; set; }

    /// <summary>
    /// Логин админа
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Email админа
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// Роль: admin, super_admin
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Отображаемое имя
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Аватар
    /// </summary>
    public string? ProfileImage { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
