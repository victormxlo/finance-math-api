using AutoMapper;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Commands.Handlers
{
    public class UnlockUserAchievementHandler : IRequestHandler<UnlockUserAchievementCommand, Result<AchievementDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGamificationProfileRepository _gamificationProfileRepository;
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;

        public UnlockUserAchievementHandler(
            IUserRepository userRepository,
            IGamificationProfileRepository gamificationProfileRepository,
            IAchievementRepository achievementRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _gamificationProfileRepository = gamificationProfileRepository;
            _achievementRepository = achievementRepository;
            _mapper = mapper;
        }

        public async Task<Result<AchievementDto>> Handle(
            UnlockUserAchievementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user == null)
                    return Result<AchievementDto>.Fail($"User not found with id: {request.UserId}.");

                var profile = await _gamificationProfileRepository.GetByUserIdAsync(user.Id);

                if (profile == null)
                    return Result<AchievementDto>.Fail($"Profile not found with user id: {user.Id}.");

                var achievement = await _achievementRepository.GetByIdAsync(request.AchievementId);

                if (achievement == null)
                    return Result<AchievementDto>.Fail($"Achievement not found with id: {request.AchievementId}.");

                profile.AddAchievement(achievement);

                await _gamificationProfileRepository.UpdateAsync(profile);

                return Result<AchievementDto>.Ok(_mapper.Map<AchievementDto>(achievement));
            }
            catch (Exception ex)
            {
                return Result<AchievementDto>.Fail($"Failed to unlock user achievement: {ex.Message}.");
            }
        }
    }
}
