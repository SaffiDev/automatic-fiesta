using DigitalStore.Infrastructure.Data.Models;

namespace DigitalStore.Infrastructure.InterfaceRepositories
{
    public interface IUserWishlistRepository
    {
        Task<List<UserWishlist>> GetByUserIdAsync(int userId);
        Task<UserWishlist?> GetByIdAsync(int id);
        Task AddAsync(UserWishlist wishlist);
        Task DeleteAsync(int id);
    }
}