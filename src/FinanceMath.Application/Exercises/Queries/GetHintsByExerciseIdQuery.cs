using FinanceMath.Application.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Exercises.Queries
{
    public class GetHintsByExerciseIdQuery : IRequest<Result<ICollection<ExerciseHintDto>>>
    {
        public Guid Id { get; set; }
    }
}
