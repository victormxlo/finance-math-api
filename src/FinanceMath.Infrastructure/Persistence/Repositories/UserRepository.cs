using FinanceMath.Domain.Repositories;
using FinanceMath.Domain.Users.Entities;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;

        public UserRepository(ISession session)
        {
            _session = session;
        }

        public async Task<User?> GetByEmailAsync(string email)
            => await _session.Query<User>()
                .FirstOrDefaultAsync(u => u.Email.Value == email);

        public async Task<User?> GetByIdAsync(Guid id)
            => await _session.GetAsync<User>(id);

        public async Task<User?> GetByUsernameAsync(string username)
            => await _session.Query<User>()
                .FirstOrDefaultAsync(u => u.Username == username);

        public async Task AddAsync(User user)
        {
            using var transaction = _session.BeginTransaction();
            await _session.SaveAsync(user);
            await transaction.CommitAsync();
        }

        public async Task UpdateAsync(User user)
        {
            using var transaction = _session.BeginTransaction();
            await _session.UpdateAsync(user);
            await transaction.CommitAsync();
        }
    }
}
