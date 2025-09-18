using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly ISession _session;

        public LevelRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<Level>> GetAllAsync()
        {
            return await _session.Query<Level>()
                .OrderBy(l => l.ThresholdExperience)
                .ToListAsync();
        }

        public async Task<Level> GetByIdAsync(int id)
        {
            return await _session.GetAsync<Level>(id);
        }

        public async Task SaveAsync(Level level)
        {
            using var tx = _session.BeginTransaction();
            await _session.SaveAsync(level);
            await tx.CommitAsync();
        }

        public async Task UpdateAsync(Level level)
        {
            using var tx = _session.BeginTransaction();
            await _session.UpdateAsync(level);
            await tx.CommitAsync();
        }
    }
}
