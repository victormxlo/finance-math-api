using FinanceMath.Application.Content.Contents.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Commands
{
    public class LinkContentExerciseCommand : IRequest<Result<LinkContentExerciseDto>>
    {
        public Guid ContentId { get; set; }
        public Guid ExerciseId { get; set; }
    }
}
