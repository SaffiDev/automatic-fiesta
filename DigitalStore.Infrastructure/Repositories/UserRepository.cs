using DigitalStore.Infrastructure.Data;
using DigitalStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalStore.Infrastructure.InterfaceRepositories;
namespace DigitalStore.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DigitalStoreDbContext _context;

        public UserRepository(DigitalStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<User1>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User1?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User1?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddAsync(User1 user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User1 user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            User1? user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}