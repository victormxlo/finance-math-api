using FinanceMath.Application.Users.Dtos;
using FinanceMath.Domain.Users;
using MediatR;

namespace FinanceMath.Application.Users.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var data = request.Request;

            //var existingUser = await _userRepository.GetByUsernameAsync(data.Username);
            //if (existingUser != null)
            //    return Result<UserDto>.Fail("Username already exists.");

            var email = new Email(data.Email);
            var user = new User(data.Username, data.FullName, email, data.Password);
            //_userRepository.Add(user);

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
