using FinanceMath.Application.Content.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Commands
{
    public class CompleteExerciseCommand : IRequest<Result<CompleteExerciseResponseDto>>
    {
        public Guid UserId { get; set; }
        public Guid ExerciseId { get; set; }
        public Guid ExerciseOptionId { get; set; }
        public bool? UsedHint { get; set; } = false;
    }
}
