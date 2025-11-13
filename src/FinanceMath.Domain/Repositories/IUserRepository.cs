using FinanceMath.Domain.Users.Entities;

namespace FinanceMath.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllAsync();
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task UpdateAsync(User user);
    }
}
