using FinanceMath.Application.Gamification.Levels.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Levels.Commands
{
    public class UpdateLevelCommand : IRequest<Result<LevelDto>>
    {
        public int Id { get; set; }
        public int? NewId { get; set; } = null;
        public required string Name { get; set; }
        public int ThresholdExperience { get; set; }
    }
}
