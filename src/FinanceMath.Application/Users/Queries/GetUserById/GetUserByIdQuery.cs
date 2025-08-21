using FinanceMath.Application.Users.Dtos;
using MediatR;

namespace FinanceMath.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Result<UserDto>>
    {
        public Guid Id { get; set; }
    }
}
