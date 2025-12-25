using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DigitalStore.Application.DTOs;

[Table("digital_content")]
public class DigitalContentDto : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("download_url")]
    public string? DownloadUrl { get; set; }

    [Column("file_path")]
    public string? FilePath { get; set; }
}