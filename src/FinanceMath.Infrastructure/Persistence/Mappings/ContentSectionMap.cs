using FinanceMath.Domain.ContentAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class ContentSectionMap : ClassMap<ContentSection>
    {
        public ContentSectionMap()
        {
            Table("content_sections");
            Id(x => x.Id)
                .GeneratedBy.GuidComb();
            Map(x => x.Title)
               .Not.Nullable();
            Map(x => x.Body)
                .CustomSqlType("text")
                .Not.Nullable();
            Map(x => x.Order)
                .Column("section_order")
                .Not.Nullable();

            References(x => x.Content)
                .Column("content_id")
                .Not.Nullable()
                .Cascade.None();
        }
    }
}
