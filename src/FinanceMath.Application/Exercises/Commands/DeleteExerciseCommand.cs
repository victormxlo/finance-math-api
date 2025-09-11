using MediatR;

namespace FinanceMath.Application.Exercises.Commands
{
    public class DeleteExerciseCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
