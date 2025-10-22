using AutoMapper;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Queries.Handlers
{
    public class GetUserAchievementProgressHandler : IRequestHandler<GetUserAchievementProgressQuery, Result<ICollection<UserAchievementProgressDto>>>
    {
        private readonly IUserAchievementProgressRepository _userAchievementProgressRepository;
        private readonly IMapper _mapper;

        public GetUserAchievementProgressHandler(IUserAchievementProgressRepository userAchievementProgressRepository, IMapper mapper)
        {
            _userAchievementProgressRepository = userAchievementProgressRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<UserAchievementProgressDto>>> Handle(GetUserAchievementProgressQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var progresses = await _userAchievementProgressRepository.GetByUserIdAsync(request.UserId);

                if (progresses is null)
                    return Result<ICollection<UserAchievementProgressDto>>.Fail($"No achievement progress found with user id: {request.UserId}.");

                var dtos = _mapper.Map<ICollection<UserAchievementProgressDto>>(progresses);

                return Result<ICollection<UserAchievementProgressDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<ICollection<UserAchievementProgressDto>>.Fail($"Failed to get user achievement progress: {ex.Message}.");
            }
        }
    }
}
