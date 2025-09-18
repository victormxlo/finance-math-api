using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class ChallengeRepository : IChallengeRepository
    {
        private readonly ISession _session;

        public ChallengeRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<Challenge>> GetActiveChallengesAsync()
        {
            var now = DateTime.UtcNow;
            return await _session.Query<Challenge>()
                .Where(c => c.StartDate <= now && c.EndDate >= now)
                .ToListAsync();
        }

        public async Task<Challenge> GetByIdAsync(Guid id)
        {
            return await _session.GetAsync<Challenge>(id);
        }

        public async Task<ICollection<Challenge>> GetAllAsync()
        {
            return await _session.Query<Challenge>().ToListAsync();
        }

        public async Task SaveAsync(Challenge challenge)
        {
            using var tx = _session.BeginTransaction();
            await _session.SaveAsync(challenge);
            await tx.CommitAsync();
        }

        public async Task UpdateAsync(Challenge challenge)
        {
            using var tx = _session.BeginTransaction();
            await _session.UpdateAsync(challenge);
            await tx.CommitAsync();
        }

        public async Task DeleteAsync(Challenge challenge)
        {
            using var tx = _session.BeginTransaction();
            await _session.DeleteAsync(challenge);
            await tx.CommitAsync();
        }
    }
}
