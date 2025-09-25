using FinanceMath.Application.Gamification.Profiles.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Profiles.Commands
{
    public class CreateGamificationProfileCommand : IRequest<Result<GamificationProfileDto>>
    {
        public Guid UserId { get; set; }
        public int? LevelId { get; set; } = null;
    }
}
