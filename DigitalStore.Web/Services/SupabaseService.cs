using Supabase;
using DigitalStore.Application.DTOs;


namespace DigitalStore.Web.Services
{
    public class SupabaseService
    {
        private readonly Client _client;

        public SupabaseService(Client client)
        {
            _client = client;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var response = await _client.From<ProductDto>().Get();
            return response.Models;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var response = await _client.From<CategoryDto>().Get();
            return response.Models;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _client.From<ProductDto>()
                    .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, id)
                    .Single();

                return product;  
            }
            catch (Exception)
            {
                return null;  // Товара нет или ошибка
            }
        }

        public async Task<List<ReviewDto>> GetReviewsAsync(int productId)
        {
            var response = await _client.From<ReviewDto>()
                .Filter("product_id", Supabase.Postgrest.Constants.Operator.Equals, productId)
                .Order("created_at", Supabase.Postgrest.Constants.Ordering.Descending)
                .Get();
            return response.Models;
        }
    }
}