using DigitalStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DigitalStore.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        // Одна UTC-константа для всех вычислений дат
        private static readonly DateTime UtcNow =
            new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Фиксированные GUID'ы
        private static readonly Guid Order1Guid = new Guid("11111111-1111-1111-1111-111111111111");
        private static readonly Guid Order2Guid = new Guid("22222222-2222-2222-2222-222222222222");
        private static readonly Guid DownloadToken1 = new Guid("33333333-3333-3333-3333-333333333333");

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Музыка", Description = "Цифровые треки и альбомы", IsActive = true, SortOrder = 1 },
                new Category { Id = 2, Name = "Книги", Description = "Электронные книги и аудиокниги", IsActive = true, SortOrder = 2 },
                new Category { Id = 3, Name = "ПО", Description = "Программное обеспечение и лицензии", IsActive = true, SortOrder = 3 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Альбом Best Hits 2025",
                    Description = "Сборник лучших треков года",
                    Price = 499.00m,
                    CategoryId = 1,
                    ProductType = "music",
                    Author = "Various Artists",
                    Language = "Русский",
                    IsActive = true,
                    StockQuantity = 1000,
                    CreatedAt = UtcNow.AddMonths(-6),
                    SalesCount = 156
                },
                new Product
                {
                    Id = 2,
                    Name = "C# 12 и .NET 8 для профессионалов",
                    Description = "Полное руководство по современному C#",
                    Price = 1499.00m,
                    CategoryId = 2,
                    ProductType = "book",
                    Author = "Марк Дж. Прайс",
                    Publisher = "ДМК Пресс",
                    Language = "Русский",
                    IsActive = true,
                    StockQuantity = 300,
                    CreatedAt = UtcNow.AddMonths(-3),
                    SalesCount = 89
                },
                new Product
                {
                    Id = 3,
                    Name = "Adobe Photoshop 2025",
                    Description = "Профессиональная годовая лицензия",
                    Price = 7999.00m,
                    CategoryId = 3,
                    ProductType = "software",
                    Version = "2025.1",
                    Publisher = "Adobe",
                    Language = "Русский",
                    IsActive = true,
                    StockQuantity = 50,
                    CreatedAt = UtcNow.AddMonths(-1),
                    SalesCount = 23
                }
            );

         
            modelBuilder.Entity<User1>().HasData(
                new User1
                {
                    Id = 1,
                    Username = "demo_user",
                    Email = "demo@example.com",
                    PasswordHash = "seeded_fake_hash_123", 
                    FirstName = "Демо",
                    LastName = "Пользователь",
                    Phone = "+7 (999) 123-45-67",
                    CreatedAt = UtcNow.AddMonths(-12),
                    UpdatedAt = UtcNow.AddMonths(-12)
                },
                new User1
                {
                    Id = 2,
                    Username = "testuser2",
                    Email = "test2@example.com",
                    PasswordHash = "another_seeded_fake_hash_456",
                    FirstName = "Тестовый",
                    LastName = "Пользователь",
                    Phone = null,
                    CreatedAt = UtcNow.AddMonths(-6),
                    UpdatedAt = UtcNow.AddMonths(-6)
                }
            );

            modelBuilder.Entity<Discount>().HasData(
                new Discount
                {
                    Id = 1,
                    Code = "WELCOME15",
                    Description = "Скидка 15% для новых покупателей",
                    DiscountType = "percentage",
                    DiscountValue = 15.00m,
                    MinPurchaseAmount = 1000.00m,
                    ValidFrom = UtcNow.AddDays(-12),
                    ValidUntil = UtcNow.AddMonths(1).AddDays(15),
                    IsActive = true,
                    CreatedAt = UtcNow.AddMonths(-1)
                },
                new Discount
                {
                    Id = 2,
                    Code = "BOOKS20",
                    Description = "20% на все книги",
                    DiscountType = "percentage",
                    DiscountValue = 20.00m,
                    ValidUntil = UtcNow.AddMonths(2).AddDays(1),
                    IsActive = true,
                    CreatedAt = UtcNow.AddMonths(-2)
                }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    ProductId = 1,
                    UserId = 1, 
                    Rating = 5,
                    Title = "Шикарный сборник!",
                    Comment = "Очень качественный звук, все хиты в одном альбоме. Рекомендую!",
                    IsVerifiedPurchase = true,
                    IsApproved = true,
                    HelpfulCount = 12,
                    CreatedAt = UtcNow.AddDays(-15)
                },
                new Review
                {
                    Id = 2,
                    ProductId = 2,
                    UserId = 1,
                    Rating = 5,
                    Title = "Лучшая книга по C#",
                    Comment = "Всё объяснено понятно, много примеров. Идеально для подготовки к работе.",
                    IsVerifiedPurchase = true,
                    IsApproved = true,
                    HelpfulCount = 25,
                    CreatedAt = UtcNow.AddDays(-8)
                },
                new Review
                {
                    Id = 3,
                    ProductId = 3,
                    UserId = 2, 
                    Rating = 4,
                    Title = "Отличный софт",
                    Comment = "Всё работает, лицензия пришла мгновенно. Минус звезда за цену.",
                    IsVerifiedPurchase = true,
                    IsApproved = true,
                    HelpfulCount = 7,
                    CreatedAt = UtcNow.AddDays(-5)
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    UserId = 1, 
                    OrderNumber = Order1Guid,
                    TotalAmount = 1998.00m,
                    DiscountAmount = 299.70m,
                    FinalAmount = 1698.30m,
                    Status = "completed",
                    DiscountCode = "WELCOME15",
                    CreatedAt = UtcNow.AddDays(-17),
                    PaidAt = UtcNow.AddDays(-17).AddHours(13),
                    CompletedAt = UtcNow.AddDays(-15)
                },
                new Order
                {
                    Id = 2,
                    UserId = 1,
                    OrderNumber = Order2Guid,
                    TotalAmount = 1499.00m,
                    DiscountAmount = 299.80m,
                    FinalAmount = 1199.20m,
                    Status = "completed",
                    DiscountCode = "BOOKS20",
                    CreatedAt = UtcNow.AddDays(-8),
                    PaidAt = UtcNow.AddDays(-8).AddHours(18).AddMinutes(30),
                    CompletedAt = UtcNow.AddDays(-7)
                }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1,
                    ProductName = "Альбом Best Hits 2025",
                    Quantity = 2,
                    UnitPrice = 499.00m,
                    TotalPrice = 998.00m,
                    DownloadsRemaining = 5
                },
                new OrderItem
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = 3,
                    ProductName = "Adobe Photoshop 2025",
                    Quantity = 1,
                    UnitPrice = 7999.00m,
                    TotalPrice = 7999.00m,
                    LicenseKey = "PS-2025-XXXX-XXXX-XXXX-XXXX",
                    DownloadToken = DownloadToken1,
                    DownloadExpiresAt = UtcNow.AddMonths(1),
                    DownloadsRemaining = 3
                },
                new OrderItem
                {
                    Id = 3,
                    OrderId = 2,
                    ProductId = 2,
                    ProductName = "C# 12 и .NET 8 для профессионалов",
                    Quantity = 1,
                    UnitPrice = 1499.00m,
                    TotalPrice = 1499.00m,
                    DownloadsRemaining = 10
                }
            );

            modelBuilder.Entity<UserWishlist>().HasData(
                new UserWishlist
                {
                    Id = 1,
                    UserId = 1, 
                    ProductId = 3,
                    AddedAt = UtcNow.AddDays(-20)
                },
                new UserWishlist
                {
                    Id = 2,
                    UserId = 1,
                    ProductId = 2,
                    AddedAt = UtcNow.AddDays(-10)
                }
            );
        }
    }
}