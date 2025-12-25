using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DigitalStore.Application.DTOs  // ← Вот это обязательно добавь!
{
    [Table("orders")]
    public class OrderDto : BaseModel
    {
        [PrimaryKey("id", false)]  // false = не вставлять это поле
        public int? Id { get; set; }   // ← Вот здесь int? вместо int
        
        [Column("auth_user_id")]
        public string? AuthUserId { get; set; }

        [Column("order_number")]
        public Guid OrderNumber { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("discount_amount")]
        public decimal DiscountAmount { get; set; }

        [Column("final_amount")]
        public decimal FinalAmount { get; set; }

        [Column("status")]
        public string Status { get; set; } = default!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [Column("paid_at")]
        public DateTime? PaidAt { get; set; }
    }
}