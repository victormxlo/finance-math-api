using FinanceMath.Application.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Exercises.Queries
{
    public class GetExerciseByIdQuery : IRequest<Result<ExerciseDto>>
    {
        public Guid Id { get; set; }
    }
}
