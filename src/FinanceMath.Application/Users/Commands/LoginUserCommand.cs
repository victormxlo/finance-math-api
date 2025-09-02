using FinanceMath.Application.Users.Dtos;
using FinanceMath.Application.Users.Requests;
using MediatR;

namespace FinanceMath.Application.Users.Commands
{
    public class LoginUserCommand : IRequest<Result<UserDto>>
    {
        public required LoginUserRequest Request { get; set; }
    }
}
