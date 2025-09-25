using AutoMapper;
using FinanceMath.Application.Gamification.Leaderboards.Dtos;
using FinanceMath.Application.Interfaces;
using MediatR;

namespace FinanceMath.Application.Gamification.Leaderboards.Queries.Handlers
{
    public class GetLeaderboardHandler : IRequestHandler<GetLeaderboardQuery, Result<ICollection<LeaderboardEntryDto>>>
    {
        private readonly IGamificationService _gamificationService;
        private readonly IMapper _mapper;

        public GetLeaderboardHandler(IGamificationService gamificationService, IMapper mapper)
        {
            _gamificationService = gamificationService;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<LeaderboardEntryDto>>> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var profiles = await _gamificationService.GetLeaderboardAsync(request.Top);

                if (profiles is null)
                    return Result<ICollection<LeaderboardEntryDto>>
                        .Fail($"No leaderboard found.");

                return Result<ICollection<LeaderboardEntryDto>>.Ok(profiles);
            }
            catch (Exception ex)
            {
                return Result<ICollection<LeaderboardEntryDto>>
                    .Fail($"Failed to get the leaderboard: {ex.Message}.");
            }
        }
    }
}
