using AutoMapper;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Queries.Handlers
{
    public class GetAchievementsByUserIdHandler : IRequestHandler<GetAchievementsByUserIdQuery, Result<ICollection<AchievementDto>>>
    {
        private readonly IUserAchievementProgressRepository _achievementProgressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAchievementsByUserIdHandler(IUserAchievementProgressRepository achievementProgressRepository, IUserRepository userRepository, IMapper mapper)
        {
            _achievementProgressRepository = achievementProgressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<AchievementDto>>> Handle(GetAchievementsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user == null)
                    return Result<ICollection<AchievementDto>>.Fail($"User not found with id: {request.UserId}.");

                var achievementProgresses = await _achievementProgressRepository.GetByUserId(user.Id);

                var achievements = achievementProgresses.Select(ap => ap.Achievement);

                if (achievements == null)
                    return Result<ICollection<AchievementDto>>.Fail($"No achievements found with user id: {user.Id}.");

                return Result<ICollection<AchievementDto>>
                    .Ok(_mapper.Map<ICollection<AchievementDto>>(achievements));
            }
            catch (Exception ex)
            {
                return Result<ICollection<AchievementDto>>.Fail($"Failed to get achievement by user id: {ex.Message}.");
            }
        }
    }
}
