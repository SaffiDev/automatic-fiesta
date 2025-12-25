using DigitalStore.Application.DTOs;
using Client = Supabase.Client;
namespace DigitalStore.Web.Services
{
    public class OrderService
    {
        private readonly SupabaseService _supabase;
        private readonly CartService _cartService;
        private readonly AuthService _authService;

        public OrderService(SupabaseService supabase, CartService cartService, AuthService authService)
        {
            _supabase = supabase;
            _cartService = cartService;
            _authService = authService;
        }
        public async Task<bool> MarkOrderAsPaidAsync(int orderId)
        {
            return await _supabase.MarkOrderAsPaidAsync(orderId);
        }
        public async Task<int?> CreateOrderFromCartAsync()
        {
            try
            {
                Console.WriteLine("=== OrderService.CreateOrderFromCartAsync ===");

                // Исправлено: правильный метод из AuthService
                var authUserId = _authService.GetUserId();

                if (string.IsNullOrEmpty(authUserId))
                {
                    Console.WriteLine("ERROR: Пользователь не авторизован");
                    return null;
                }

                Console.WriteLine($"auth_user_id: {authUserId}");

                var cartItems = await _cartService.GetCartItemsWithProductsAsync();
                Console.WriteLine($"Товаров в корзине: {cartItems.Count}");

                if (cartItems.Count == 0)
                {
                    Console.WriteLine("ERROR: Корзина пуста");
                    return null;
                }

                var totalAmount = await _cartService.GetTotalPriceAsync();
                Console.WriteLine($"Общая сумма: {totalAmount:F2}");

                // Исправлено: CreateOrderAsync теперь принимает только cartItems и totalAmount
                // auth_user_id берётся внутри SupabaseService из AuthService
                var order = await _supabase.CreateOrderAsync(cartItems, totalAmount);

                if (order != null && order.Id.HasValue)
                {
                    Console.WriteLine($"Заказ создан! ID: {order.Id.Value}");
                    await _cartService.ClearLocalAsync(); // очищаем корзину
                    Console.WriteLine("Корзина очищена");
                    return order.Id.Value;
                }
                else
                {
                    Console.WriteLine("ERROR: order == null или Id не вернулся");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ОШИБКА в OrderService: {ex.Message}");
                Console.WriteLine($"!!! StackTrace: {ex.StackTrace}");
                return null;
            }
        }
    }
}