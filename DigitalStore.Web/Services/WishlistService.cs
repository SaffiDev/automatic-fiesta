using DigitalStore.Application.DTOs;
using static Supabase.Postgrest.Constants;

namespace DigitalStore.Web.Services;

public class WishlistService
{
    private readonly Supabase.Client _client;
    private readonly AuthService _authService;

    public WishlistService(Supabase.Client client, AuthService authService)
    {
        _client = client;
        _authService = authService;
    }

    // Получить товары из избранного
    public async Task<List<ProductDto>> GetWishlistProductsAsync()
    {
        var userId = _authService.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return new List<ProductDto>();

        // 1. Получаем wishlist
        var wishlistResponse = await _client
            .From<WishlistDto>()
            .Filter("auth_user_id", Operator.Equals, userId)
            .Get();

        var productIds = wishlistResponse.Models
            .Select(w => w.ProductId)
            .ToList();

        if (productIds.Count == 0)
            return new List<ProductDto>();

        // 2. ВАЖНО: Operator.In + List<int>
        var productsResponse = await _client
            .From<ProductDto>()
            .Filter("id", Operator.In, productIds)
            .Get();

        return productsResponse.Models;
    }


    // Добавить в избранное
    public async Task<bool> AddToWishlistAsync(int productId)
    {
        var userId = _authService.GetUserId();
        if (string.IsNullOrEmpty(userId)) return false;

        try
        {
            var item = new WishlistDto
            {
                AuthUserId = userId,
                ProductId = productId
            };

            await _client.From<WishlistDto>().Insert(item);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // Удалить из избранного
    public async Task<bool> RemoveFromWishlistAsync(int productId)
    {
        var userId = _authService.GetUserId();
        if (string.IsNullOrEmpty(userId)) return false;

        try
        {
            await _client
                .From<WishlistDto>()
                .Filter("auth_user_id", Operator.Equals, userId)
                .Filter("product_id", Operator.Equals, productId)
                .Delete();

            return true;
        }
        catch
        {
            return false;
        }
    }

    // Проверить, есть ли в избранном
    public async Task<bool> IsInWishlistAsync(int productId)
    {
        var userId = _authService.GetUserId();
        if (string.IsNullOrEmpty(userId)) return false;

        try
        {
            var response = await _client
                .From<WishlistDto>()
                .Filter("auth_user_id", Operator.Equals, userId)
                .Filter("product_id", Operator.Equals, productId)
                .Get();

            return response.Models.Any();
        }
        catch
        {
            return false;
        }
    }
}