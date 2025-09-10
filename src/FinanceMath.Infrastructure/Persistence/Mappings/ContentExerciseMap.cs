using FinanceMath.Domain.ContentAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class ContentExerciseMap : ClassMap<ContentExercise>
    {
        public ContentExerciseMap()
        {
            Table("content_exercises");

            Id(x => x.Id).GeneratedBy.GuidComb();

            References(x => x.Content)
                .Column("content_id")
                .Not.Nullable()
                .Cascade.None();

            References(x => x.Exercise)
                .Column("exercise_id")
                .Not.Nullable()
                .Cascade.None();
        }
    }
}
