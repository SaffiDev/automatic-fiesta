using DigitalStore.Infrastructure.Data.Models;

//Для User1
namespace DigitalStore.Infrastructure.InterfaceRepositories
{
    public interface IUserRepository
    {
        Task<List<User1>> GetAllAsync();
        Task<User1?> GetByIdAsync(int id);
        Task<User1?> GetByUsernameAsync(string username);
        Task AddAsync(User1 user);
        Task UpdateAsync(User1 user);
        Task DeleteAsync(int id);
    }
}