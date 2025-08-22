using FinanceMath.Domain.Entities.Users;
using FinanceMath.Domain.Repositories;
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

        public async Task AddAsync(User entity)
        {
            using var transaction = _session.BeginTransaction();
            await _session.SaveAsync(entity);
            await transaction.CommitAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
            => await _session.GetAsync<User>(id);

        public async Task<User?> GetByUsername(string username)
            => await _session.Query<User>()
                .FirstOrDefaultAsync(u => u.Username == username);
    }
}
