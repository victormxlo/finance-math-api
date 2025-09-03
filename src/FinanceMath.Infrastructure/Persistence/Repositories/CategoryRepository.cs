using FinanceMath.Domain.ContentAggregate;
using FinanceMath.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FinanceMath.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ISession _session;

        public CategoryRepository(ISession session)
        {
            _session = session;
        }

        public async Task<ICollection<Category>> GetAllAsync()
            => await _session.Query<Category>()
                .Fetch(c => c.Subcategories)
                .ToListAsync();

        public async Task<Category?> GetByIdAsync(Guid id)
            => await _session.Query<Category>()
                .Fetch(c => c.ParentCategory)
                .FetchMany(c => c.Subcategories)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task SaveAsync(Category category)
        {
            using var transaction = _session.BeginTransaction();
            await _session.SaveAsync(category);
            await transaction.CommitAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            using var transaction = _session.BeginTransaction();
            await _session.UpdateAsync(category);
            await transaction.CommitAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            using var transaction = _session.BeginTransaction();
            await _session.DeleteAsync(category);
            await transaction.CommitAsync();
        }
    }
}
