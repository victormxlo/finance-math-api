using AutoMapper;
using FinanceMath.Application.Gamification.Profiles.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Profiles.Queries.Handlers
{
    public class GetGamificationProfileByUserIdHandler : IRequestHandler<GetGamificationProfileByUserIdQuery, Result<GamificationProfileDto>>
    {
        private readonly IGamificationProfileRepository _gamificationProfileRepository;
        private readonly IMapper _mapper;

        public GetGamificationProfileByUserIdHandler(IGamificationProfileRepository gamificationProfileRepository, IMapper mapper)
        {
            _gamificationProfileRepository = gamificationProfileRepository;
            _mapper = mapper;
        }

        public async Task<Result<GamificationProfileDto>> Handle(GetGamificationProfileByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await _gamificationProfileRepository.GetByUserIdAsync(request.UserId);

                if (profile == null)
                    return Result<GamificationProfileDto>.Fail($"Gamification profile not found with user id: {request.UserId}.");

                return Result<GamificationProfileDto>.Ok(_mapper.Map<GamificationProfileDto>(profile));
            }
            catch (Exception ex)
            {
                return Result<GamificationProfileDto>.Fail($"Failed to get gamification profile by user id: {ex.Message}.");
            }
        }
    }
}
