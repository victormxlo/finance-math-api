using FinanceMath.Domain.GamificationAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class GamificationProfileMap : ClassMap<GamificationProfile>
    {
        public GamificationProfileMap()
        {
            Table("gamification_profiles");
            Id(x => x.Id)
                .CustomSqlType("uuid")
                .GeneratedBy.GuidComb();

            Map(x => x.ExperiencePoints)
                .Column("experience_points")
                .Not.Nullable();
            Map(x => x.VirtualCurrency)
                .Column("virtual_currency")
                .Not.Nullable();

            Map(x => x.CurrentStreakDays)
                .Column("current_streak_days")
                .Not.Nullable();
            Map(x => x.LastActivityDate)
                .Column("last_activity_date");

            References(x => x.User)
                .Column("user_id")
                .Unique()
                .Not.Nullable()
                .Cascade.None();

            References(x => x.Level)
                .Column("level_id")
                .Not.Nullable()
                .Cascade.None();

            HasMany(x => x.Achievements)
                .KeyColumn("achievement_id")
                .Inverse()
                .Cascade.AllDeleteOrphan();
        }
    }
}
