using FinanceMath.Application.Content.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Commands
{
    public class ValidateExerciseAnswerCommand : IRequest<Result<ValidateExerciseAnswerDto>>
    {
        public Guid UserId { get; set; }
        public Guid ExerciseId { get; set; }
        public Guid ExerciseOptionId { get; set; }
    }
}
