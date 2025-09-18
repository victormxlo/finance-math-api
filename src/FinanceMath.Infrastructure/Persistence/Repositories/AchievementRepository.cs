using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class AchievementRepository : IAchievementRepository
    {
        private readonly ISession _session;

        public AchievementRepository(ISession session)
        {
            _session = session;
        }

        public async Task<Achievement> GetByIdAsync(Guid id)
        {
            return await _session.GetAsync<Achievement>(id);
        }

        public async Task<ICollection<Achievement>> GetByCriteriaAsync(string criteriaKey)
        {
            if (string.IsNullOrWhiteSpace(criteriaKey))
                return new List<Achievement>();

            return await _session.Query<Achievement>()
                .Where(a => a.CriteriaKey == criteriaKey)
                .ToListAsync();
        }

        public async Task<ICollection<Achievement>> GetAllAsync()
        {
            return await _session.Query<Achievement>()
                .ToListAsync();
        }

        public async Task SaveAsync(Achievement achievement)
        {
            using var tx = _session.BeginTransaction();
            await _session.SaveAsync(achievement);
            await tx.CommitAsync();
        }

        public async Task UpdateAsync(Achievement achievement)
        {
            using var tx = _session.BeginTransaction();
            await _session.UpdateAsync(achievement);
            await tx.CommitAsync();
        }
    }
}
