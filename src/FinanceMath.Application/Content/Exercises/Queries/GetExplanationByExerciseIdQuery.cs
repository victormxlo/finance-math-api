using MediatR;

namespace FinanceMath.Application.Content.Exercises.Queries
{
    public class GetExplanationByExerciseIdQuery : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }
}
