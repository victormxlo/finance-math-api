using FinanceMath.Application.Gamification.Profiles.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Profiles.Commands
{
    public class UpdateGamificationProfileCommand : IRequest<Result<GamificationProfileDto>>
    {
        public Guid UserId { get; set; }
        public int? ExperiencePoints { get; set; }
        public int? VirtualCurrency { get; set; }
        public int? LevelId { get; set; }
        public DateTime? ActivityDate { get; set; }
    }
}
