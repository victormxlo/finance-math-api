using FinanceMath.Domain.GamificationAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class ChallengeMap : ClassMap<Challenge>
    {
        public ChallengeMap()
        {
            Table("challenges");

            Id(x => x.Id)
                .CustomSqlType("uuid")
                .GeneratedBy.Assigned();

            Map(x => x.Name)
                .Column("name")
                .Length(100)
                .Not.Nullable();

            Map(x => x.Description)
                .Column("description")
                .Not.Nullable();

            Map(x => x.CriteriaKey)
                .Column("criteria_key")
                .Length(100)
                .Not.Nullable();

            Map(x => x.Target)
                .Column("target")
                .Not.Nullable();

            Map(x => x.ExperienceReward)
                .Column("experience_reward")
                .Not.Nullable();

            Map(x => x.VirtualCurrencyReward)
                .Column("virtual_currency_reward")
                .Not.Nullable();

            Map(x => x.StartDate)
                .Column("start_date")
                .Not.Nullable();

            Map(x => x.EndDate)
                .Column("end_date")
                .Not.Nullable();

            Map(x => x.CreatedAt)
                .Column("created_at")
                .Not.Nullable();
        }
    }
}
