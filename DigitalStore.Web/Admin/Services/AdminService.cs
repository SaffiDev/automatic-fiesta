using System.Net.Http.Json;
using DigitalStore.Application.DTOs;


namespace DigitalStore.Web.Admin.Services;

public class AdminService
{
    private readonly HttpClient _http;
    private const string ApiBase = "http://localhost:5090/api/admin";

    public AdminService(HttpClient http)
    {
        _http = http;
    }

    public async Task<AdminStats> GetStatsAsync()
    {
        try
        {
            // Загружаем продукты
            var products = await _http.GetFromJsonAsync<List<ProductDto>>($"{ApiBase}/products");
            products ??= new List<ProductDto>(); // если null — пустой список

            // Загружаем заказы
            var orders = await _http.GetFromJsonAsync<List<OrderDto>>($"{ApiBase}/orders");
            orders ??= new List<OrderDto>();

            return new AdminStats
            {
                ProductsCount = products.Count,
                OrdersCount = orders.Count,
                PaidOrdersCount = orders.Count(o => o.Status == "paid")
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("AdminService GetStats error: " + ex.Message);
            return new AdminStats(); // возвращаем пустую статистику при ошибке
        }
    }

    // Вынес класс AdminStats наружу (лучшая практика) или оставь внутри — но с public конструктором
    public class AdminStats
    {
        public int ProductsCount { get; set; }
        public int OrdersCount { get; set; }
        public int PaidOrdersCount { get; set; }
    }
}