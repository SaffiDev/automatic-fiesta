using Blazored.LocalStorage;
using DigitalStore.Web.WebModels;
using DigitalStore.Application.DTOs;
namespace DigitalStore.Web.Services;

public class CartService
{
    private const string LocalStorageKey = "digitalstore_cart";
        private readonly ILocalStorageService _localStorage;
        private readonly SupabaseService _supabase;  // Реализация позже(не трогать, он будет позже использован, наверное)

        public CartService(ILocalStorageService localStorage, SupabaseService supabase)
        {
            _localStorage = localStorage;
            _supabase = supabase;
        }

        public event Action? OnChange;

        // Локальная часть(гость) 
        public async Task<List<CartItem>> GetLocalItemsAsync()
        {
            var items = await _localStorage.GetItemAsync<List<CartItem>>(LocalStorageKey);
            return items ?? new List<CartItem>();
        }

        public async Task AddProductAsync(ProductDto product, int quantity = 1)
        {
            var cart = await GetLocalItemsAsync();
            var cartProduct = new CartProductDto(product);  // Копируем только нужные данные

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
    //TODO: Реализация регистрации на сервере и сохранение корзины в бдшке(сейчас хранение не в бд а в Blazored.LocalStorage 
    
    //Прикол в том что хранится локально в браузере но корзина теряется при очистке кэша, в режиме инкогнито или на другом устройстве(у нас пока режим гостя,регистрации нет)
        // Та самая реализация позже для сервера
        // public async Task SyncWithServerAsync() { ... }
        // public async Task LoadFromServerAsync() { ... }
    }
