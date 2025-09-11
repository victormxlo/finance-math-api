using FinanceMath.Application.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Exercises.Commands
{
    public class CreateExerciseCommand : IRequest<Result<ExerciseDto>>
    {
        public string Question { get; set; } = null!;
        public string Explanation { get; set; } = null!;
        public string Difficulty { get; set; } = null!;

        public ICollection<CreateExerciseOptionDto> Options { get; set; } = new List<CreateExerciseOptionDto>();
        public ICollection<CreateExerciseHintDto> Hints { get; set; } = new List<CreateExerciseHintDto>();
    }
}
