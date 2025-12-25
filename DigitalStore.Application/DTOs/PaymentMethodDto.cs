using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DigitalStore.Application.DTOs;

[Table("user_payment_methods")]
public class PaymentMethodDto : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("card_type")]
    public string CardType { get; set; } = "";

    [Column("last_four")]
    public string LastFour { get; set; } = "";

    [Column("expiry_date")]
    public string ExpiryDate { get; set; } = "";

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
