using AutoMapper;
using FinanceMath.Application.Gamification.Profiles.Dtos;
using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Profiles.Commands.Handlers
{
    public class CreateGamificationProfileHandler : IRequestHandler<CreateGamificationProfileCommand, Result<GamificationProfileDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IGamificationProfileRepository _gamificationProfileRepository;
        private readonly IMapper _mapper;

        public CreateGamificationProfileHandler(
            IUserRepository userRepository,
            ILevelRepository levelRepository,
            IGamificationProfileRepository gamificationProfileRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _levelRepository = levelRepository;
            _gamificationProfileRepository = gamificationProfileRepository;
            _mapper = mapper;
        }

        public async Task<Result<GamificationProfileDto>> Handle(CreateGamificationProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user is null)
                    return Result<GamificationProfileDto>
                        .Fail($"User not found with id: {request.UserId}.");

                Level level;
                if (request.LevelId.HasValue)
                {
                    level = await _levelRepository.GetByIdAsync(request.LevelId.Value);

                    if (level is null)
                        return Result<GamificationProfileDto>
                            .Fail($"Level not found with id: {request.LevelId}.");
                }
                else
                {
                    level = await _levelRepository.GetInitialLevelAsync();

                    if (level is null)
                        return Result<GamificationProfileDto>
                            .Fail($"No starting level available.");
                }

                var profile = new GamificationProfile(user, level);

                await _gamificationProfileRepository.AddAsync(profile);

                return Result<GamificationProfileDto>.Ok(
                    _mapper.Map<GamificationProfileDto>(profile));
            }
            catch (Exception ex)
            {
                return Result<GamificationProfileDto>
                    .Fail($"Failed to create gamification profile: {ex.Message}.");
            }
        }
    }
}
