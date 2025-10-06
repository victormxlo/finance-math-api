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
        {
            var exercise = await _session.Query<Exercise>()
               .Where(e => e.Id == id)
               .FetchMany(e => e.Options)
               .SingleOrDefaultAsync();

            if (exercise == null)
                return null;

            await _session.Query<ExerciseHint>()
                .Where(h => h.Exercise.Id == id)
                .ToListAsync();

            await _session.Query<ContentExercise>()
                .Where(c => c.Exercise.Id == id)
                .ToListAsync();

            return exercise;
        }

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
