using FinanceMath.Domain.GamificationAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class UserExerciseProgressMap : ClassMap<UserExerciseProgress>
    {
        public UserExerciseProgressMap()
        {
            Table("user_exercise_progresses");

            CompositeId()
                .KeyReference(x => x.GamificationProfile, "gamification_profile_id")
                .KeyReference(x => x.Exercise, "exercise_id");

            Map(x => x.CompletedAt)
                .Column("completed_at")
                .Not.Nullable();
        }
    }
}
