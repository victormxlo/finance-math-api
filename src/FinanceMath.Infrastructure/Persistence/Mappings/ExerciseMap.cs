using FinanceMath.Domain.ContentAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class ExerciseMap : ClassMap<Exercise>
    {
        public ExerciseMap()
        {
            Table("exercises");

            Id(x => x.Id)
                .GeneratedBy.GuidComb();

            Map(x => x.Question)
                .CustomSqlType("text")
                .Not.Nullable();

            Map(x => x.Explanation)
                .CustomSqlType("text")
                .Not.Nullable();

            Map(x => x.Difficulty)
                .Not.Nullable();

            Map(x => x.CreatedAt)
                .Not.Nullable();

            Map(x => x.UpdatedAt)
                .Nullable();

            HasMany(x => x.Options)
                .KeyColumn("exercise_id")
                .Inverse()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.Hints)
                .KeyColumn("exercise_id")
                .Inverse()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.ContentExercises)
                .KeyColumn("exercise_id")
                .Inverse()
                .Cascade.AllDeleteOrphan();
        }
    }
}
