using DigitalStore.Infrastructure.Data.Models;

namespace DigitalStore.Infrastructure.InterfaceRepositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<List<Review>> GetByProductIdAsync(int productId);
        Task<Review?> GetByIdAsync(int id);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
    }
}