using DigitalStore.Infrastructure.Data;
using DigitalStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalStore.Infrastructure.InterfaceRepositories;
namespace DigitalStore.Infrastructure.Repositories
{
    public class UserWishlistRepository : IUserWishlistRepository
    {
        private readonly DigitalStoreDbContext _context;

        public UserWishlistRepository(DigitalStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserWishlist>> GetByUserIdAsync(int userId)
        {
            return await _context.UserWishlists.Where(w => w.UserId == userId).Include(w => w.Product).ToListAsync();
        }

        public async Task<UserWishlist?> GetByIdAsync(int id)
        {
            return await _context.UserWishlists.Include(w => w.Product).FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task AddAsync(UserWishlist wishlist)
        {
            _context.UserWishlists.Add(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var wishlist = await GetByIdAsync(id);
            if (wishlist != null)
            {
                _context.UserWishlists.Remove(wishlist);
                await _context.SaveChangesAsync();
            }
        }
    }
}