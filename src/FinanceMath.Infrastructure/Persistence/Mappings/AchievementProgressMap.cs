using FinanceMath.Domain.GamificationAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class AchievementProgressMap : ClassMap<AchievementProgress>
    {
        public AchievementProgressMap()
        {
            Table("achievement_progresses");

            CompositeId()
                .KeyReference(x => x.GamificationProfile, "gamification_profile_id")
                .KeyReference(x => x.Achievement, "achievement_id");

            Map(x => x.UnlockedAt)
                .Column("unlocked_at")
                .Not.Nullable();
        }
    }
}
