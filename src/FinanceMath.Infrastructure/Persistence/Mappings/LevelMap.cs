using FinanceMath.Domain.GamificationAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class LevelMap : ClassMap<Level>
    {
        public LevelMap()
        {
            Table("levels");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Assigned();

            Map(x => x.Name)
                .Column("name")
                .Length(100)
                .Not.Nullable();

            Map(x => x.ThresholdExperience)
                .Column("threshold_experience")
                .Not.Nullable();
        }
    }
}
