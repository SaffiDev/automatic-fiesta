using Microsoft.AspNetCore.Mvc;
using Supabase;
using DigitalStore.Application.DTOs;

[ApiController]
[Route("api/admin/order-items")]
public class AdminOrderItemsController : ControllerBase
{
    private readonly Client _client;

    public AdminOrderItemsController(Client client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var response = await _client.From<OrderItemDto>().Get();
            
            var items = response.Models.Select(i => new
            {
                id = i.Id,
                orderId = i.OrderId,
                productId = i.ProductId,
                productName = i.ProductName,
                quantity = i.Quantity,
                unitPrice = i.UnitPrice,
                totalPrice = i.TotalPrice,
                downloadToken = i.DownloadToken,
                downloadExpiresAt = i.DownloadExpiresAt,
                downloadsRemaining = i.DownloadsRemaining
            }).ToList();
            
            return Ok(items);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}