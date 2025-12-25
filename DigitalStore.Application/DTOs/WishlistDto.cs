using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DigitalStore.Application.DTOs;

[Table("user_wishlist")]
public class WishlistDto : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("auth_user_id")]
    public string? AuthUserId { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("added_at")]
    public DateTime AddedAt { get; set; }
}