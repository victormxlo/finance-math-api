using FinanceMath.Domain.GamificationAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class UserAchievementProgressMap : ClassMap<UserAchievementProgress>
    {
        public UserAchievementProgressMap()
        {
            Table("user_achievement_progresses");

            CompositeId()
                .KeyReference(x => x.GamificationProfile, "gamification_profile_id")
                .KeyReference(x => x.Achievement, "achievement_id");

            Map(x => x.UnlockedAt)
                .Column("unlocked_at")
                .Not.Nullable();
        }
    }
}
