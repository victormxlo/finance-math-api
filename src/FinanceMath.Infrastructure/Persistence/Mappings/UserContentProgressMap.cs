using FinanceMath.Domain.GamificationAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class UserContentProgressMap : ClassMap<UserContentProgress>
    {
        public UserContentProgressMap()
        {
            Table("user_content_progresses");

            CompositeId()
                .KeyReference(x => x.GamificationProfile, "gamification_profile_id")
                .KeyReference(x => x.Content, "content_id");

            Map(x => x.CompletedAt)
                .Column("completed_at")
                .Not.Nullable();
        }
    }
}
