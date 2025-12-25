// ProductResponseDto.cs
namespace DigitalStore.Application.DTOs;

public class ProductResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string ProductType { get; set; } = string.Empty;
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public string? Version { get; set; }
    public string? Language { get; set; }
    public bool IsActive { get; set; }
    public int SalesCount { get; set; }
    public int StockQuantity { get; set; }
}