using AutoMapper;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Queries.Handlers
{
    public class GetAchievementByIdHandler : IRequestHandler<GetAchievementByIdQuery, Result<AchievementDto>>
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;

        public GetAchievementByIdHandler(IAchievementRepository achievementRepository, IMapper mapper)
        {
            _achievementRepository = achievementRepository;
            _mapper = mapper;
        }

        public async Task<Result<AchievementDto>> Handle(GetAchievementByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var achievement = await _achievementRepository.GetByIdAsync(request.Id);

                if (achievement == null)
                    return Result<AchievementDto>.Fail($"Achievement not found with id: {request.Id}.");

                var dto = _mapper.Map<AchievementDto>(achievement);

                return Result<AchievementDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<AchievementDto>.Fail($"Failed to get achievement by id: {ex.Message}.");
            }
        }
    }
}
