using FinanceMath.Application.Users.Dtos;
using MediatR;

namespace FinanceMath.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<Result<UserDto>>
    {
        public Guid Id { get; set; }
    }
}
