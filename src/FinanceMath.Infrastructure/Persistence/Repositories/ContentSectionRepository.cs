using FinanceMath.Domain.ContentAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class ContentSectionRepository : IContentSectionRepository
    {
        private readonly ISession _session;

        public ContentSectionRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<ContentSection>> GetAllByContentIdAsync(Guid contentId)
            => await _session.Query<ContentSection>()
                .Where(cs => cs.Content.Id == contentId)
                .OrderBy(cs => cs.Order)
                .ToListAsync();

        public async Task<ContentSection?> GetByIdAsync(Guid id)
            => await _session.Query<ContentSection>()
                .FirstOrDefaultAsync(cs => cs.Id == id);

        public async Task SaveAsync(ContentSection contentSection)
        {
            using var transaction = _session.BeginTransaction();
            await _session.SaveAsync(contentSection);
            await transaction.CommitAsync();
        }

        public async Task UpdateAsync(ContentSection contentSection)
        {
            using var transaction = _session.BeginTransaction();
            await _session.UpdateAsync(contentSection);
            await transaction.CommitAsync();
        }

        public async Task DeleteAsync(ContentSection contentSection)
        {
            using var transaction = _session.BeginTransaction();
            await _session.DeleteAsync(contentSection);
            await transaction.CommitAsync();
        }
    }
}
