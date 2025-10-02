using FinanceMath.Domain.ContentAggregate;
using FluentNHibernate.Mapping;
namespace FinanceMath.Infrastructure.Persistence.Mappings
{
    public class ExerciseOptionMap : ClassMap<ExerciseOption>
    {
        public ExerciseOptionMap()
        {
            Table("exercise_options");

            Id(x => x.Id)
                .CustomSqlType("uuid")
                .GeneratedBy.Assigned();

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

            Map(x => x.IsCorrect)
                .Column("is_correct")
                .Not.Nullable();
        }
    }
}
