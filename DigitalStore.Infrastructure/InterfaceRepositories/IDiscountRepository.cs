using DigitalStore.Infrastructure.Data.Models;

namespace DigitalStore.Infrastructure.InterfaceRepositories
{
    public interface IDiscountRepository
    {
        Task<List<Discount>> GetAllAsync();
        Task<Discount?> GetByIdAsync(int id);
        Task<Discount?> GetByCodeAsync(string code);
        Task AddAsync(Discount discount);
        Task UpdateAsync(Discount discount);
        Task DeleteAsync(int id);
    }
}