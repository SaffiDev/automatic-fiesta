using DigitalStore.Infrastructure.Data.Models;

namespace DigitalStore.Infrastructure.InterfaceRepositories
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetByOrderIdAsync(int orderId);
        Task<OrderItem?> GetByIdAsync(int id);
        Task AddAsync(OrderItem orderItem);
        Task UpdateAsync(OrderItem orderItem);
        Task DeleteAsync(int id);
    }
}