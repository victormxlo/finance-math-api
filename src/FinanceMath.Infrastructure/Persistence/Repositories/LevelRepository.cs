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

        public async Task<Level> GetInitialLevelAsync()
        {
            var initial = await _session.Query<Level>()
                .OrderBy(l => l.ThresholdExperience)
                .FirstOrDefaultAsync();

            if (initial != null)
                return initial;

            return await GetByIdAsync(1);
        }

        public async Task SaveAsync(Level level)
        {
            using var transaction = _session.BeginTransaction();
            await _session.SaveAsync(level);
            await transaction.CommitAsync();
        }

        public async Task UpdateAsync(Level level)
        {
            using var transaction = _session.BeginTransaction();
            await _session.UpdateAsync(level);
            await transaction.CommitAsync();
        }

        public async Task DeleteAsync(Level level)
        {
            using var transaction = _session.BeginTransaction();
            await _session.DeleteAsync(level);
            await transaction.CommitAsync();
        }
    }
}
