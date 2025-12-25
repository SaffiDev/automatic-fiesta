using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models; 
namespace DigitalStore.Application.DTOs;

[Table("payment_transactions")]
public class PaymentTransactionDto : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("transaction_id")]
    public string TransactionId { get; set; } = "";

    [Column("payment_system")]
    public string PaymentSystem { get; set; } = "demo";

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("status")]
    public string Status { get; set; } = "success";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}