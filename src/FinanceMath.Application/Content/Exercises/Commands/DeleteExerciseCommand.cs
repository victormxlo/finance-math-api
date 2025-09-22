using MediatR;

namespace FinanceMath.Application.Content.Exercises.Commands
{
    public class DeleteExerciseCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
