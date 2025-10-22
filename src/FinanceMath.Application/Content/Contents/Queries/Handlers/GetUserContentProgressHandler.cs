using AutoMapper;
using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Queries.Handlers
{
    public class GetUserContentProgressHandler : IRequestHandler<GetUserContentProgressQuery, Result<ICollection<UserContentProgressDto>>>
    {
        private readonly IUserContentProgressRepository _userContentProgressRepository;
        private readonly IMapper _mapper;

        public GetUserContentProgressHandler(IUserContentProgressRepository userContentProgressRepository, IMapper mapper)
        {
            _userContentProgressRepository = userContentProgressRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<UserContentProgressDto>>> Handle(GetUserContentProgressQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var progresses = await _userContentProgressRepository.GetByUserIdAsync(request.UserId);

                if (progresses is null)
                    return Result<ICollection<UserContentProgressDto>>.Fail($"No content progress found with user id: {request.UserId}.");

                var dtos = _mapper.Map<ICollection<UserContentProgressDto>>(progresses);

                return Result<ICollection<UserContentProgressDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<ICollection<UserContentProgressDto>>.Fail($"Failed to get user content progress: {ex.Message}.");
            }
        }
    }
}
