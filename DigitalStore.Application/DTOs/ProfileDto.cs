using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DigitalStore.Application.DTOs;

[Table("profiles")]
public class ProfileDto : BaseModel
{
    [PrimaryKey("id", false)]  
    public int Id { get; set; }

    [Column("auth_user_id")]
    public string AuthUserId { get; set; } = "";

    [Column("email")]
    public string? Email { get; set; }

    [Column("role")]
    public string Role { get; set; } = "user";

}