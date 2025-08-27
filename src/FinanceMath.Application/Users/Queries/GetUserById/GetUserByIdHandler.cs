using FinanceMath.Application.Interfaces;
using FinanceMath.Application.Users.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Users.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = request.Id;
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null || user?.Id != userId)
                return Result<UserDto>.Fail("User not found.");

            var dto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email.Value,
                CreatedAt = user.CreatedAt
            };

            return Result<UserDto>.Ok(dto);
        }
    }
}
