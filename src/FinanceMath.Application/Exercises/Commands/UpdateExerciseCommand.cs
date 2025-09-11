using FinanceMath.Application.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Exercises.Commands
{
    public class UpdateExerciseCommand : IRequest<Result<ExerciseDto>>
    {
        public Guid Id { get; set; }
        public string Question { get; set; } = null!;
        public string Explanation { get; set; } = null!;
        public string Difficulty { get; set; } = null!;
        public ICollection<UpdateExerciseOptionDto> Options { get; set; } = new List<UpdateExerciseOptionDto>();
        public ICollection<UpdateExerciseHintDto> Hints { get; set; } = new List<UpdateExerciseHintDto>();
    }
}
