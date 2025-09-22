using FinanceMath.Application.Content.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Queries
{
    public class GetExerciseByIdQuery : IRequest<Result<ExerciseDto>>
    {
        public Guid Id { get; set; }
    }
}
