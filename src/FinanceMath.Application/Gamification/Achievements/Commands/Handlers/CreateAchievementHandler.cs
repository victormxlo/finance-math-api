using AutoMapper;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Commands.Handlers
{
    public class CreateAchievementHandler : IRequestHandler<CreateAchievementCommand, Result<AchievementDto>>
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;

        public CreateAchievementHandler(IAchievementRepository achievementRepository, IMapper mapper)
        {
            _achievementRepository = achievementRepository;
            _mapper = mapper;
        }

        public async Task<Result<AchievementDto>> Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Achievement achievement = new Achievement(
                    request.Name, request.Description, request.CriteriaKey,
                    request.ExperienceReward, request.VirtualCurrencyReward);

                await _achievementRepository.SaveAsync(achievement);

                var dto = _mapper.Map<AchievementDto>(achievement);

                return Result<AchievementDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<AchievementDto>.Fail($"Failed to create achievement: {ex.Message}.");
            }
        }
    }
}
