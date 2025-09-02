using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IContentRepository
    {
        Task<ICollection<Content>> GetAllAsync();
        Task<Content?> GetByIdAsync(Guid id);
        Task SaveAsync(Content content);
        Task UpdateAsync(Content content);
        Task DeleteAsync(Content content);
    }
}
