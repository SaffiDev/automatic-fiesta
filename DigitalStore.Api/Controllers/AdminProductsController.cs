using Microsoft.AspNetCore.Mvc;
using Supabase;
using DigitalStore.Application.DTOs;
using static Supabase.Postgrest.Constants;

[ApiController]
[Route("api/admin/products")]
public class AdminProductsController : ControllerBase
{
    private readonly Client _client;

    public AdminProductsController(Client client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var response = await _client.From<ProductDto>().Get();

            var products = response.Models.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                description = p.Description,
                price = p.Price,
                categoryId = p.CategoryId,
                productType = p.ProductType,
                author = p.Author,
                publisher = p.Publisher,
                version = p.Version,
                language = p.Language,
                isActive = p.IsActive,
                salesCount = p.SalesCount,
                stockQuantity = p.StockQuantity
            }).ToList();

            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto product)
    {
        try
        {
            var response = await _client.From<ProductDto>().Insert(product);
            var created = response.Models.FirstOrDefault();

            return Ok(new
            {
                id = created?.Id,
                name = created?.Name
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDto product)
    {
        try
        {
            product.Id = id;
            await _client.From<ProductDto>().Update(product);
            return Ok(new { message = "Product updated" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            Console.WriteLine($"Deleting product with ID: {id}");

            // Сначала получаем продукт
            var response = await _client
                .From<ProductDto>()
                .Filter("id", Operator.Equals, id)
                .Get();

            if (response.Models.Count == 0)
            {
                return NotFound(new { message = "Product not found" });
            }

            var product = response.Models.First();

            // Теперь удаляем
            await _client.From<ProductDto>().Delete(product);

            return Ok(new { message = "Product deleted" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return StatusCode(500, new { message = ex.Message, details = ex.ToString() });
        }
    }
}