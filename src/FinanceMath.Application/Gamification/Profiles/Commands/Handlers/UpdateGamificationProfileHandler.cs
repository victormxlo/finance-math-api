using AutoMapper;
using FinanceMath.Application.Gamification.Profiles.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Profiles.Commands.Handlers
{
    public class UpdateGamificationProfileHandler : IRequestHandler<UpdateGamificationProfileCommand, Result<GamificationProfileDto>>
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IGamificationProfileRepository _gamificationProfileRepository;
        private readonly IMapper _mapper;

        public UpdateGamificationProfileHandler(
            ILevelRepository levelRepository,
            IGamificationProfileRepository gamificationProfileRepository,
            IMapper mapper)
        {
            _levelRepository = levelRepository;
            _gamificationProfileRepository = gamificationProfileRepository;
            _mapper = mapper;
        }

        public async Task<Result<GamificationProfileDto>> Handle(UpdateGamificationProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await _gamificationProfileRepository.GetByUserIdAsync(request.UserId);
                if (profile == null)
                    return Result<GamificationProfileDto>
                        .Fail($"Gamification profile with user id: {request.UserId} not found.");

                if (request.ExperiencePoints.HasValue)
                    profile.AddExperience(request.ExperiencePoints.Value);

                if (request.VirtualCurrency.HasValue)
                    profile.AddVirtualCurrency(request.VirtualCurrency.Value);

                if (request.LevelId.HasValue)
                {
                    var level = await _levelRepository.GetByIdAsync(request.LevelId.Value);
                    if (level == null)
                        return Result<GamificationProfileDto>
                            .Fail($"Level {request.LevelId} não encontrado.");

                    profile.UpdateLevel(level);
                }

                if (request.ActivityDate.HasValue)
                    profile.UpdateStreak(request.ActivityDate.Value);

                await _gamificationProfileRepository.UpdateAsync(profile);

                return Result<GamificationProfileDto>.Ok(
                    _mapper.Map<GamificationProfileDto>(profile));
            }
            catch (Exception ex)
            {
                return Result<GamificationProfileDto>
                    .Fail($"Failed to update gamification profile: {ex.Message}.");
            }
        }
    }
}
