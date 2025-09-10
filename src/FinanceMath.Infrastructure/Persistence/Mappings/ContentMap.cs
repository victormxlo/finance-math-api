using FinanceMath.Domain.ContentAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class ContentMap : ClassMap<Content>
    {
        public ContentMap()
        {
            Table("contents");
            Id(x => x.Id)
                .CustomSqlType("uuid")
                .GeneratedBy.GuidComb();
            Map(x => x.Title)
                .Not.Nullable();

            Map(x => x.Body)
                .CustomSqlType("text")
                .Not.Nullable();

            Map(x => x.MediaUrl)
                .Column("media_url");

            Map(x => x.CreatedBy)
                .Column("created_by")
                .Not.Nullable();

            Map(x => x.CreatedAt)
                .Column("created_at")
                .Not.Nullable();

            Map(x => x.UpdatedAt)
                .Column("updated_at");

            References(x => x.Category)
                .Column("category_id")
                .Not.Nullable()
                .Cascade.None();

            HasMany(x => x.ContentExercises)
                .KeyColumn("content_id")
                .Inverse()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.Sections)
                .KeyColumn("content_id")
                .Inverse()
                .Cascade.AllDeleteOrphan();
        }
    }
}
