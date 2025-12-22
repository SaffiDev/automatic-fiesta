using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DigitalStore.Application.DTOs;

[Table("products")]
public class ProductDto : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("category_id")]
    public int CategoryId { get; set; }
    
    [Column("product_type")]
    public string ProductType { get; set; } = string.Empty;
    
    [Column("author")]
    public string? Author { get; set; }
    
    [Column("publisher")]
    public string? Publisher { get; set; }
    
    [Column("version")]
    public string? Version { get; set; }
    
    [Column("language")]
    public string? Language { get; set; }
    
    [Column("is_active")]
    public bool IsActive { get; set; }
    
    [Column("sales_count")]
    public int SalesCount { get; set; }
    
    [Column("stock_quantity")]
    public int StockQuantity { get; set; }
}