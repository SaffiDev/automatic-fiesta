using DigitalStore.Application.DTOs;
namespace DigitalStore.Web.WebModels
{
    public class CartProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? ProductType { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        // Пустой конструктор (обязателен для десериализации)
        public CartProductDto() { }

        // Конструктор для удобного копирования (оставляем, но он не мешает)
        public CartProductDto(ProductDto product)
        {
            Id = product.Id;
            Name = product.Name;
            Author = product.Author;
            Publisher = product.Publisher;
            ProductType = product.ProductType;
            Price = product.Price;
            Description = product.Description;
        }
    }
}