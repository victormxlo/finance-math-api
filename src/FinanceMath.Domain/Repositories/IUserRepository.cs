using FinanceMath.Domain.Entities.Users;

namespace FinanceMath.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User entity);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUsername(string username);
    }
}
