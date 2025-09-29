using FinanceMath.Domain.GamificationAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class UserChallengeProgressMap : ClassMap<UserChallengeProgress>
    {
        public UserChallengeProgressMap()
        {
            Table("user_challenge_progresses");

            CompositeId()
                    .KeyReference(x => x.GamificationProfile, "gamification_profile_id")
                    .KeyReference(x => x.Challenge, "challenge_id");

            Map(x => x.CompletedAt)
                    .Column("completed_at")
                    .Not.Nullable();
        }
    }
}
