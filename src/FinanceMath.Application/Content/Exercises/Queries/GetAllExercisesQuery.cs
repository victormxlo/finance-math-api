using FinanceMath.Application.Content.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Queries
{
    public class GetAllExercisesQuery : IRequest<Result<ICollection<ExerciseDto>>> { }
}
