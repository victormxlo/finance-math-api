using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class GamificationProfileRepository : IGamificationProfileRepository
    {
        private readonly ISession _session;

        public GamificationProfileRepository(ISession session)
        {
            _session = session;
        }

        public async Task<GamificationProfile> GetByUserIdAsync(Guid userId)
        {
            return await _session.Query<GamificationProfile>()
                .Fetch(p => p.User)
                .FetchMany(p => p.Achievements).ThenFetch(ap => ap.Achievement)
                .Where(p => p.User.Id == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<GamificationProfile> GetByIdAsync(Guid id)
        {
            return await _session.Query<GamificationProfile>()
                .Fetch(p => p.User)
                .FetchMany(p => p.Achievements).ThenFetch(ap => ap.Achievement)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(GamificationProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            using var tx = _session.BeginTransaction();
            await _session.SaveAsync(profile);
            await tx.CommitAsync();
        }

        public async Task UpdateAsync(GamificationProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            using var tx = _session.BeginTransaction();
            await _session.UpdateAsync(profile);
            await tx.CommitAsync();
        }

        public async Task<ICollection<GamificationProfile>> GetTopByExperienceAsync(int top)
        {
            if (top <= 0) top = 50;

            return await _session.Query<GamificationProfile>()
                .Fetch(p => p.User)
                .OrderByDescending(p => p.ExperiencePoints)
                .Take(top)
                .ToListAsync();
        }
    }
}
