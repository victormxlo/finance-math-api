using FinanceMath.Application.Content.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Queries
{
    public class GetHintsByExerciseIdQuery : IRequest<Result<ICollection<ExerciseHintDto>>>
    {
        public Guid Id { get; set; }
    }
}
