using FinanceMath.Domain.Users.Entities;

namespace FinanceMath.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User entity);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
    }
}
