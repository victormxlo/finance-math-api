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
                .CustomSqlType("uuid")
                .GeneratedBy.Assigned();

            Map(x => x.Question)
                .CustomSqlType("text")
                .Not.Nullable();

            Map(x => x.Explanation)
                .CustomSqlType("text")
                .Not.Nullable();

            Map(x => x.Difficulty)
                .Not.Nullable();

            Map(x => x.CreatedAt)
                .Column("created_at")
                .Not.Nullable();

            Map(x => x.UpdatedAt)
                .Column("updated_at")
                .Nullable();

            HasMany(x => x.Options)
                .KeyColumn("exercise_id")
                .Inverse()
                .AsSet()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.Hints)
                .KeyColumn("exercise_id")
                .Inverse()
                .AsSet()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.ContentExercises)
                .KeyColumn("exercise_id")
                .Inverse()
                .AsSet()
                .Cascade.AllDeleteOrphan();
        }
    }
}
