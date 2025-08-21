using FinanceMath.Application.Users.Dtos;
using MediatR;

namespace FinanceMath.Application.Users.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = new UserDto
            {
                Id = request.Id,
                Username = "demo",
                FullName = "Demo User",
                Email = "demo@email.com",
                CreatedAt = DateTime.UtcNow
            };

            return Result<UserDto>.Ok(user);
        }
    }
}
