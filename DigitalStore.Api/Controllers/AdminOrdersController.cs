using Microsoft.AspNetCore.Mvc;
using Supabase;
using DigitalStore.Application.DTOs;

[ApiController]
[Route("api/admin/orders")]
public class AdminOrdersController : ControllerBase
{
    private readonly Client _client;

    public AdminOrdersController(Client client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var response = await _client.From<OrderDto>().Get();
            
            var orders = response.Models.Select(o => new
            {
                id = o.Id,
                authUserId = o.AuthUserId,
                orderNumber = o.OrderNumber,
                totalAmount = o.TotalAmount,
                discountAmount = o.DiscountAmount,
                finalAmount = o.FinalAmount,
                status = o.Status,
                createdAt = o.CreatedAt,
                paidAt = o.PaidAt
            }).ToList();
            
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}