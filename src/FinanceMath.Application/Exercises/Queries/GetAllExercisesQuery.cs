using FinanceMath.Application.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Exercises.Queries
{
    public class GetAllExercisesQuery : IRequest<Result<ICollection<ExerciseDto>>> { }
}
