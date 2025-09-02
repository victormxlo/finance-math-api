using FinanceMath.Application.Users.Dtos;
using FinanceMath.Application.Users.Requests;
using MediatR;

namespace FinanceMath.Application.Users.Commands
{
    public class RegisterUserCommand : IRequest<Result<UserDto>>
    {
        public required RegisterUserRequest Request { get; set; }
    }
}
