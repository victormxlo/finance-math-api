using AutoMapper;
using FinanceMath.Application.Gamification.Profiles.Dtos;
using FinanceMath.Application.Interfaces;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Profiles.Commands.Handlers
{
    public class GrantUserVirtualCurrencyHandler : IRequestHandler<GrantUserVirtualCurrencyCommand, Result<GamificationProfileDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGamificationProfileRepository _gamificationProfileRepository;
        private readonly IGamificationService _gamificationService;
        private readonly IMapper _mapper;

        public GrantUserVirtualCurrencyHandler(
            IUserRepository userRepository,
            IGamificationProfileRepository gamificationProfileRepository,
            IGamificationService gamificationService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _gamificationProfileRepository = gamificationProfileRepository;
            _gamificationService = gamificationService;
            _mapper = mapper;
        }

        public async Task<Result<GamificationProfileDto>> Handle(GrantUserVirtualCurrencyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user == null)
                    return Result<GamificationProfileDto>.Fail($"User not found with id: {request.UserId}.");

                var profile = await _gamificationProfileRepository.GetByUserIdAsync(user.Id);

                if (profile == null)
                    return Result<GamificationProfileDto>.Fail($"Profile not found with user id: {user.Id}.");

                await _gamificationService.AwardVirtualCurrencyAsync(user.Id, request.VirtualCurrencyAmount);

                return Result<GamificationProfileDto>
                    .Ok(_mapper.Map<GamificationProfileDto>(profile));
            }
            catch (Exception ex)
            {
                return Result<GamificationProfileDto>.Fail($"Failed to grant user virtual currency: {ex.Message}.");
            }
        }
    }
}
