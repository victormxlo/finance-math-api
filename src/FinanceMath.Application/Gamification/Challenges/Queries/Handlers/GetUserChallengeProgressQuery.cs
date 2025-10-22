using AutoMapper;
using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Queries.Handlers
{
    public class GetUserChallengeProgressHandler : IRequestHandler<GetUserChallengeProgressQuery, Result<ICollection<UserChallengeProgressDto>>>
    {
        private readonly IUserChallengeProgressRepository _userChallengeProgressRepository;
        private readonly IMapper _mapper;

        public GetUserChallengeProgressHandler(IUserChallengeProgressRepository userChallengeProgressRepository, IMapper mapper)
        {
            _userChallengeProgressRepository = userChallengeProgressRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<UserChallengeProgressDto>>> Handle(GetUserChallengeProgressQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var progresses = await _userChallengeProgressRepository.GetByUserIdAsync(request.UserId);

                if (progresses is null)
                    return Result<ICollection<UserChallengeProgressDto>>.Fail($"No challenge progress found with user id: {request.UserId}.");

                var dtos = _mapper.Map<ICollection<UserChallengeProgressDto>>(progresses);

                return Result<ICollection<UserChallengeProgressDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<ICollection<UserChallengeProgressDto>>.Fail($"Failed to get user challenge progress: {ex.Message}.");
            }
        }
    }
}
