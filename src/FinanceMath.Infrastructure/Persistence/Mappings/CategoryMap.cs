using FinanceMath.Domain.ContentAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Table("categories");
            Id(x => x.Id)
                .CustomSqlType("uuid")
                .GeneratedBy.GuidComb();
            Map(x => x.Name)
                .Not.Nullable();

            References(x => x.ParentCategory)
                .Column("parent_category_id")
                .Cascade.None()
                .Nullable();

            HasMany(x => x.Subcategories)
                .KeyColumn("parent_category_id")
                .Inverse()
                .AsSet()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.Contents)
                .KeyColumn("category_id")
                .Inverse()
                .AsSet()
                .Cascade.None();
        }
    }
}
