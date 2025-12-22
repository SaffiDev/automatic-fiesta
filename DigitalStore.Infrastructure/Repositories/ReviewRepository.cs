using DigitalStore.Infrastructure.Data;
using DigitalStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalStore.Infrastructure.InterfaceRepositories;
namespace DigitalStore.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DigitalStoreDbContext _context;

        public ReviewRepository(DigitalStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.Include(r => r.Product).Include(r => r.User).ToListAsync();
        }

        public async Task<List<Review>> GetByProductIdAsync(int productId)
        {
            return await _context.Reviews.Where(r => r.ProductId == productId).Include(r => r.User).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.Include(r => r.Product).Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await GetByIdAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}