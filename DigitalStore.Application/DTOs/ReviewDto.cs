using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DigitalStore.Application.DTOs;

[Table("reviews")]
public class ReviewDto : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("product_id")]
    public int ProductId { get; set; }
    
    [Column("rating")]
    public int Rating { get; set; }
    
    [Column("title")]
    public string Title { get; set; } = string.Empty;
    
    [Column("comment")]
    public string? Comment { get; set; }
    
    [Column("is_verified_purchase")]
    public bool IsVerifiedPurchase { get; set; }
    
    [Column("helpful_count")]
    public int HelpfulCount { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}