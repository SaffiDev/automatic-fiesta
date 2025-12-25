using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DigitalStore.Application.DTOs
{
    [Table("order_items")]
    public class OrderItemDto : BaseModel
    {
        [PrimaryKey("id", false)]  // false = не вставлять это поле
        public int? Id { get; set; }   // ← Вот здесь int? вместо int

        [Column("order_id")] public int OrderId { get; set; }

        [Column("product_id")] public int ProductId { get; set; }

        [Column("product_name")] public string ProductName { get; set; } = default!;

        [Column("quantity")] public int Quantity { get; set; }

        [Column("unit_price")] public decimal UnitPrice { get; set; }

        [Column("total_price")] public decimal TotalPrice { get; set; }

        [Column("download_token")] public Guid DownloadToken { get; set; }

        [Column("download_expires_at")] public DateTime DownloadExpiresAt { get; set; }

        [Column("downloads_remaining")] public int DownloadsRemaining { get; set; }
    }
}