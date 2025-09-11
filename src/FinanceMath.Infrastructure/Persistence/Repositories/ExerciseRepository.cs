using FinanceMath.Domain.ContentAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ISession _session;

        public ExerciseRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<Exercise>> GetAllAsync()
            => await _session.Query<Exercise>()
                .FetchMany(e => e.Options)
                .FetchMany(e => e.Hints)
                .FetchMany(e => e.ContentExercises)
                .ToListAsync();

        public async Task<Exercise?> GetByIdAsync(Guid id)
            => await _session.Query<Exercise>()
                .FetchMany(e => e.Options)
                .FetchMany(e => e.Hints)
                .FetchMany(e => e.ContentExercises)
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task SaveAsync(Exercise exercise)
        {
            using var transaction = _session.BeginTransaction();
            await _session.SaveAsync(exercise);
            await transaction.CommitAsync();
        }

        public async Task UpdateAsync(Exercise exercise)
        {
            using var transaction = _session.BeginTransaction();
            await _session.UpdateAsync(exercise);
            await transaction.CommitAsync();
        }

        public async Task DeleteAsync(Exercise exercise)
        {
            using var transaction = _session.BeginTransaction();
            await _session.DeleteAsync(exercise);
            await transaction.CommitAsync();
        }
    }
}
