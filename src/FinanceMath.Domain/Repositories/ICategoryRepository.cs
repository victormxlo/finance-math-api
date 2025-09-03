using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task SaveAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
