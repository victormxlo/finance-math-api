using AutoMapper;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Queries.Handlers
{
    public class GetAllAchievementsHandler : IRequestHandler<GetAllAchievementsQuery, Result<ICollection<AchievementDto>>>
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;

        public GetAllAchievementsHandler(IAchievementRepository achievementRepository, IMapper mapper)
        {
            _achievementRepository = achievementRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<AchievementDto>>> Handle(GetAllAchievementsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var achievements = await _achievementRepository.GetAllAsync();

                if (achievements == null || achievements?.Count == 0)
                    return Result<ICollection<AchievementDto>>.Fail("Achievements not found.");

                var dtos = _mapper.Map<ICollection<AchievementDto>>(achievements);

                return Result<ICollection<AchievementDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<ICollection<AchievementDto>>.Fail($"Failed to get all achievements: {ex.Message}.");
            }
        }
    }
}
