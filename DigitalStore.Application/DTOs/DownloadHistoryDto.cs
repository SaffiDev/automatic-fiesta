using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DigitalStore.Application.DTOs;
[Table("download_history")]
public class DownloadHistoryDto : BaseModel
{
    [Column("auth_user_id")]
    public string? AuthUserId { get; set; }

    [Column("order_item_id")]
    public int OrderItemId { get; set; }

    [Column("downloaded_at")]
    public DateTime DownloadedAt { get; set; }
}