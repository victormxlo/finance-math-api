using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Domain.Repositories
{
    public interface IContentSectionRepository
    {
        Task<ICollection<ContentSection>> GetAllByContentIdAsync(Guid contentId);
        Task<ContentSection?> GetByIdAsync(Guid id);
        Task SaveAsync(ContentSection contentSection);
        Task UpdateAsync(ContentSection contentSection);
        Task DeleteAsync(ContentSection contentSection);
    }
}
