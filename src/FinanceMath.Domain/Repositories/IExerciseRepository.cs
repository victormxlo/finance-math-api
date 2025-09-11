using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IExerciseRepository
    {
        Task<ICollection<Exercise>> GetAllAsync();
        Task<Exercise?> GetByIdAsync(Guid id);
        Task SaveAsync(Exercise exercise);
        Task UpdateAsync(Exercise exercise);
        Task DeleteAsync(Exercise exercise);
    }
}
