using AutoMapper;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Commands.Handlers
{
    public class UpdateAchievementHandler : IRequestHandler<UpdateAchievementCommand, Result<AchievementDto>>
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;

        public UpdateAchievementHandler(IAchievementRepository achievementRepository, IMapper mapper)
        {
            _achievementRepository = achievementRepository;
            _mapper = mapper;
        }

        public async Task<Result<AchievementDto>> Handle(UpdateAchievementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var achievement = await _achievementRepository.GetByIdAsync(request.Id);

                if (achievement == null)
                    return Result<AchievementDto>.Fail($"Achievement not found with id: {request.Id}.");

                achievement.Update(
                    request.Name, request.Description, request.CriteriaKey,
                    request.ExperienceReward, request.VirtualCurrencyReward);

                await _achievementRepository.UpdateAsync(achievement);

                var dto = _mapper.Map<AchievementDto>(achievement);

                return Result<AchievementDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<AchievementDto>.Fail($"Failed to update achievement: {ex.Message}.");
            }
        }
    }
}
