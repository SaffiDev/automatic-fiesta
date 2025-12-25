using Supabase;
using DigitalStore.Application.DTOs;
using static DigitalStore.Web.Services.CartService;
using static Supabase.Postgrest.Constants;

namespace DigitalStore.Web.Services;

public class SupabaseService
{
    private readonly Client _client;
    private readonly AuthService _authService;

    public SupabaseService(Client client, AuthService authService)
    {
        _client = client;
        _authService = authService;
    }

    // ===================== PRODUCTS =====================
    public async Task<List<ProductDto>> GetProductsAsync()
    {
        var response = await _client.From<ProductDto>().Get();
        return response.Models;
    }

    public async Task<List<ProductDto>> SearchProductsAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Trim().Length < 2) return new List<ProductDto>();

        var response = await _client
            .From<ProductDto>()
            .Filter("name", Operator.ILike, $"%{query.Trim()}%")
            .Limit(20)
            .Get();

        return response.Models;
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        try
        {
            var response = await _client
                .From<ProductDto>()
                .Filter("id", Operator.Equals, id)
                .Get();

            return response.Models.FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }

    // ===================== CATEGORIES =====================
    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var response = await _client.From<CategoryDto>().Get();
        return response.Models;
    }

    // ===================== REVIEWS =====================
    public async Task<List<ReviewDto>> GetProductReviewsAsync(int productId)
    {
        try
        {
            var response = await _client
                .From<ReviewDto>()
                .Filter("product_id", Operator.Equals, productId)
                .Order("created_at", Ordering.Descending)
                .Get();

            return response.Models ?? new List<ReviewDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading reviews: {ex.Message}");
            return new List<ReviewDto>();
        }
    }

    public async Task<bool> AddReviewAsync(ReviewDto review)
    {
        try
        {
            review.CreatedAt = DateTime.UtcNow;
            review.AuthUserId = _authService.GetUserId();

            if (!string.IsNullOrEmpty(review.AuthUserId))
            {
                review.IsVerifiedPurchase = await CanUserReviewProductAsync(review.AuthUserId, review.ProductId);
            }

            await _client.From<ReviewDto>().Insert(review);
            return true;
        }
        catch (Exception ex)
        {
            // Ловим именно дубликат отзыва
            if (ex.Message.Contains("23505") || ex.Message.Contains("duplicate key"))
            {
                Console.WriteLine("Review already exists (duplicate key)");
                return true; // Считаем успешным — отзыв уже есть
            }

            Console.WriteLine($"Error adding review: {ex.Message}");
            return false;
        }
    }
    public async Task<double> GetAverageRatingAsync(int productId)
    {
        try
        {
            var reviews = await GetProductReviewsAsync(productId);
            return reviews.Any() ? reviews.Average(r => r.Rating) : 0;
        }
        catch
        {
            return 0;
        }
    }

    public async Task<bool> HasUserReviewedProductAsync(int productId, string userId)
    {
        try
        {
            var response = await _client
                .From<ReviewDto>()
                .Filter("product_id", Operator.Equals, productId)
                .Filter("auth_user_id", Operator.Equals, userId)
                .Get();

            return response.Models?.Any() ?? false;
        }
        catch
        {
            return false;
        }
    }

    // Проверка, может ли пользователь оставить отзыв (купил товар)
    public async Task<bool> CanUserReviewProductAsync(string authUserId, int productId)
    {
        try
        {
            var orders = await GetOrdersByUserAsync(authUserId);
            var paidOrderIds = orders
                .Where(o => o.Status == "paid" && o.Id.HasValue)
                .Select(o => o.Id.Value)
                .ToList();

            if (!paidOrderIds.Any()) return false;

            var orderIdsString = $"({string.Join(",", paidOrderIds)})";

            var itemsResponse = await _client
                .From<OrderItemDto>()
                .Filter("order_id", Operator.In, orderIdsString)
                .Filter("product_id", Operator.Equals, productId)
                .Get();

            return itemsResponse.Models.Any();
        }
        catch
        {
            return false;
        }
    }

    // ===================== ORDERS =====================
    public async Task<List<OrderDto>> GetOrdersByUserAsync(string authUserId)
    {
        var response = await _client
            .From<OrderDto>()
            .Filter("auth_user_id", Operator.Equals, authUserId)
            .Order("created_at", Ordering.Descending)
            .Get();

        return response.Models;
    }

    public async Task<bool> MarkOrderAsPaidAsync(int orderId)
    {
        try
        {
            var response = await _client
                .From<OrderDto>()
                .Where(x => x.Id == orderId)
                .Set(x => x.Status, "paid")
                .Set(x => x.PaidAt, DateTime.UtcNow)
                .Update();

            return response.Models.Any();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SupabaseService.MarkOrderAsPaidAsync error: {ex.Message}");
            return false;
        }
    }

    public async Task<List<OrderItemDto>> GetOrderItemsAsync(int orderId)
    {
        var response = await _client
            .From<OrderItemDto>()
            .Filter("order_id", Operator.Equals, orderId)
            .Get();

        return response.Models;
    }

    public async Task<OrderDto?> CreateOrderAsync(List<CartItemWithProduct> cartItems, decimal totalAmount)
    {
        try
        {
            Console.WriteLine("=== SUPABASE CreateOrderAsync ===");
            Console.WriteLine($"Items: {cartItems?.Count ?? 0}");
            Console.WriteLine($"Total: {totalAmount}");

            if (cartItems == null || cartItems.Count == 0)
            {
                Console.WriteLine("ERROR: cartItems empty");
                return null;
            }

            var authUserId = _authService.GetUserId();
            if (string.IsNullOrEmpty(authUserId))
            {
                Console.WriteLine("ERROR: Пользователь не авторизован");
                return null;
            }

            var order = new OrderDto
            {
                AuthUserId = authUserId,
                OrderNumber = Guid.NewGuid(),
                TotalAmount = totalAmount,
                DiscountAmount = 0,
                FinalAmount = totalAmount,
                Status = "pending",
                CreatedAt = DateTime.UtcNow
            };

            var orderResponse = await _client.From<OrderDto>().Insert(order);
            var savedOrder = orderResponse.Models.FirstOrDefault();

            if (savedOrder?.Id == null)
            {
                Console.WriteLine("CRITICAL ERROR: order not created");
                return null;
            }

            var orderId = savedOrder.Id.Value;

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItemDto
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price,
                    TotalPrice = item.Price * item.Quantity,
                    DownloadToken = Guid.NewGuid(),
                    DownloadExpiresAt = DateTime.UtcNow.AddDays(30),
                    DownloadsRemaining = 5
                };

                await _client.From<OrderItemDto>().Insert(orderItem);
            }

            Console.WriteLine("ORDER CREATED WITH STATUS 'pending' — READY FOR PAYMENT");
            return savedOrder;
        }
        catch (Exception ex)
        {
            Console.WriteLine("SUPABASE CREATE ORDER ERROR");
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    // ===================== PAYMENT METHODS =====================
    public async Task<List<PaymentMethodDto>> GetPaymentMethodsAsync()
    {
        try
        {
            var response = await _client
                .From<PaymentMethodDto>()
                .Order("created_at", Ordering.Descending)
                .Get();

            return response.Models;
        }
        catch
        {
            return new();
        }
    }

    public async Task<PaymentMethodDto?> AddPaymentMethodAsync(PaymentMethodDto method)
    {
        try
        {
            var response = await _client
                .From<PaymentMethodDto>()
                .Insert(method);

            return response.Models.FirstOrDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine("AddPaymentMethod ERROR:");
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> DeletePaymentMethodAsync(Guid id)
    {
        try
        {
            await _client
                .From<PaymentMethodDto>()
                .Filter("id", Operator.Equals, id)
                .Delete();

            return true;
        }
        catch
        {
            return false;
        }
    }

    // ===================== DOWNLOAD =====================
    public async Task<string?> GetDownloadUrlAsync(Guid downloadToken)
    {
        try
        {
            var itemResponse = await _client
                .From<OrderItemDto>()
                .Filter("download_token", Operator.Equals, downloadToken.ToString())
                .Single();

            if (itemResponse == null || itemResponse.DownloadsRemaining <= 0 || itemResponse.DownloadExpiresAt < DateTime.UtcNow)
            {
                return null;
            }

            var orderResponse = await _client
                .From<OrderDto>()
                .Filter("id", Operator.Equals, itemResponse.OrderId)
                .Single();

            if (orderResponse?.Status != "paid")
            {
                return null;
            }

            itemResponse.DownloadsRemaining--;
            await _client.From<OrderItemDto>().Update(itemResponse);

            var contentResponse = await _client
                .From<DigitalContentDto>()
                .Filter("product_id", Operator.Equals, itemResponse.ProductId)
                .Single();

            return contentResponse?.DownloadUrl;
        }
        catch
        {
            return null;
        }
    }

    // ===================== TRANSACTIONS =====================
    public async Task<List<PaymentTransactionDto>> GetTransactionsAsync(string authUserId)
    {
        try
        {
            var orders = await GetOrdersByUserAsync(authUserId);
            var orderIds = orders.Where(o => o.Id.HasValue).Select(o => o.Id.Value).ToList();

            if (!orderIds.Any()) return new();

            var orderIdsString = $"({string.Join(",", orderIds)})";

            var response = await _client
                .From<PaymentTransactionDto>()
                .Filter("order_id", Operator.In, orderIdsString)
                .Get();

            return response.Models;
        }
        catch
        {
            return new();
        }
    }
}