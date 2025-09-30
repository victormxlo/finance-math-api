using FinanceMath.Domain.ContentAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class ContentExerciseMap : ClassMap<ContentExercise>
    {
        public ContentExerciseMap()
        {
            Table("content_exercises");

            CompositeId()
                .KeyReference(x => x.Content, "content_id")
                .KeyReference(x => x.Exercise, "exercise_id");
        }
    }
}
