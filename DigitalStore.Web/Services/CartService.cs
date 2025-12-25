using Blazored.LocalStorage;
using DigitalStore.Web.WebModels;
using DigitalStore.Application.DTOs;
using static Supabase.Postgrest.Constants;

namespace DigitalStore.Web.Services;

public class CartService
{
    private const string LocalStorageKey = "digitalstore_cart";
    private readonly ILocalStorageService _localStorage;
    private readonly SupabaseService _supabase;

    public CartService(ILocalStorageService localStorage, SupabaseService supabase)
    {
        _localStorage = localStorage;
        _supabase = supabase;
    }

    public event Action? OnChange;

    // ============ ЛОКАЛЬНАЯ ЧАСТЬ (ГОСТЬ) ============
    public async Task<List<CartItem>> GetLocalItemsAsync()
    {
        var items = await _localStorage.GetItemAsync<List<CartItem>>(LocalStorageKey);
        return items ?? new List<CartItem>();
    }

    public async Task AddProductAsync(ProductDto product, int quantity = 1)
    {
        var cart = await GetLocalItemsAsync();
        var cartProduct = new CartProductDto(product);

        var existing = cart.FirstOrDefault(x => x.Product.Id == product.Id);

        if (existing != null)
            existing.Quantity += quantity;
        else
            cart.Add(new CartItem { Product = cartProduct, Quantity = quantity });

        await _localStorage.SetItemAsync(LocalStorageKey, cart);
        OnChange?.Invoke();
    }

    public async Task UpdateQuantityAsync(int productId, int quantity)
    {
        if (quantity <= 0)
        {
            await RemoveProductAsync(productId);
            return;
        }

        var cart = await GetLocalItemsAsync();
        var item = cart.FirstOrDefault(x => x.Product.Id == productId);
        if (item != null)
        {
            item.Quantity = quantity;
            await _localStorage.SetItemAsync(LocalStorageKey, cart);
            OnChange?.Invoke();
        }
    }

    public async Task RemoveProductAsync(int productId)
    {
        var cart = await GetLocalItemsAsync();
        cart.RemoveAll(x => x.Product.Id == productId);
        await _localStorage.SetItemAsync(LocalStorageKey, cart);
        OnChange?.Invoke();
    }

    public async Task<decimal> GetTotalPriceAsync()
    {
        var cart = await GetLocalItemsAsync();
        return cart.Sum(x => x.Product.Price * x.Quantity);
    }

    public async Task<int> GetTotalCountAsync()
    {
        var cart = await GetLocalItemsAsync();
        return cart.Sum(x => x.Quantity);
    }

    public async Task ClearLocalAsync()
    {
        await _localStorage.RemoveItemAsync(LocalStorageKey);
        OnChange?.Invoke();
    }

    // ============ МЕТОДЫ ДЛЯ ЗАКАЗОВ ============
    public async Task<List<CartItemWithProduct>> GetCartItemsWithProductsAsync()
    {
        var localCart = await GetLocalItemsAsync();
        var result = new List<CartItemWithProduct>();

        foreach (var item in localCart)
        {
            var product = await _supabase.GetProductByIdAsync(item.Product.Id);
            
            if (product != null)
            {
                result.Add(new CartItemWithProduct
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    Price = product.Price,
                    Product = product
                });
            }
        }

        return result;
    }

    // ПЕРЕМЕСТИЛ КЛАСС ВНУТРЬ CartService
    public class CartItemWithProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductDto? Product { get; set; }
    }
}