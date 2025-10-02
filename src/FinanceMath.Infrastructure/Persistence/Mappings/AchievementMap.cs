using FinanceMath.Domain.GamificationAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class AchievementMap : ClassMap<Achievement>
    {
        public AchievementMap()
        {
            Table("achievements");

            Id(x => x.Id)
                .CustomSqlType("uuid")
                .GeneratedBy.Assigned();

            Map(x => x.Name)
                .Column("name").Length(100)
                .Not.Nullable();

            Map(x => x.Description)
                .Column("description")
                .Not.Nullable();

            Map(x => x.CriteriaKey)
                .Column("criteria_key").Length(100)
                .Not.Nullable();

            Map(x => x.ExperienceReward)
                .Column("experience_reward")
                .Not.Nullable();

            Map(x => x.VirtualCurrencyReward)
                .Column("virtual_currency_reward")
                .Not.Nullable();

            Map(x => x.CreatedAt)
                .Column("created_at")
                .Not.Nullable();
        }
    }
}
