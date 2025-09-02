using FinanceMath.Domain.ContentAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly ISession _session;

        public ContentRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<Content>> GetAllAsync()
            => await _session.Query<Content>()
                .Fetch(c => c.Category)
                .FetchMany(c => c.Sections)
                .FetchMany(c => c.Exercises)
                .ToListAsync();

        public async Task<Content?> GetByIdAsync(Guid id)
            => await _session.Query<Content>()
                .Fetch(c => c.Category)
                .FetchMany(c => c.Sections)
                .FetchMany(c => c.Exercises)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task SaveAsync(Content content)
        {
            using var transaction = _session.BeginTransaction();
            await _session.SaveAsync(content);
            await transaction.CommitAsync();
        }

        public async Task UpdateAsync(Content content)
        {
            using var transaction = _session.BeginTransaction();
            await _session.UpdateAsync(content);
            await transaction.CommitAsync();
        }

        public async Task DeleteAsync(Content content)
        {
            using var transaction = _session.BeginTransaction();
            await _session.DeleteAsync(content);
            await transaction.CommitAsync();
        }
    }
}
