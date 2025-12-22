using DigitalStore.Infrastructure.Data;
using DigitalStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalStore.Infrastructure.InterfaceRepositories;
namespace DigitalStore.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DigitalStoreDbContext _context;

        public DiscountRepository(DigitalStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Discount>> GetAllAsync()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<Discount?> GetByIdAsync(int id)
        {
            return await _context.Discounts.FindAsync(id);
        }

        public async Task<Discount?> GetByCodeAsync(string code)
        {
            return await _context.Discounts.FirstOrDefaultAsync(d => d.Code == code);
        }

        public async Task AddAsync(Discount discount)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Discount discount)
        {
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var discount = await GetByIdAsync(id);
            if (discount != null)
            {
                _context.Discounts.Remove(discount);
                await _context.SaveChangesAsync();
            }
        }
    }
}