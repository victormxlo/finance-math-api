using MediatR;

namespace FinanceMath.Application.Exercises.Queries
{
    public class GetExplanationByExerciseIdQuery : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }
}
