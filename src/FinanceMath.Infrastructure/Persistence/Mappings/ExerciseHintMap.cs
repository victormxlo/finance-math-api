using FinanceMath.Domain.ContentAggregate;
using FluentNHibernate.Mapping;

namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class ExerciseHintMap : ClassMap<ExerciseHint>
    {
        public ExerciseHintMap()
        {
            Table("exercise_hints");

            Id(x => x.Id)
                .CustomSqlType("uuid")
                .GeneratedBy.GuidComb();

            References(x => x.Exercise)
                .Column("exercise_id")
                .Not.Nullable()
                .Cascade.None();

            Map(x => x.Description)
                .CustomSqlType("text")
                .Not.Nullable();

            Map(x => x.Order)
                .Column("sort_order")
                .Not.Nullable();
        }
    }
}
