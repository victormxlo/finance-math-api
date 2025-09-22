using FinanceMath.Application.Gamification.Profiles.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Profiles.Commands
{
    public class UpdateGamificationProfileCommand : IRequest<Result<GamificationProfileDto>>
    {
        public Guid UserId { get; set; }
    }
}
