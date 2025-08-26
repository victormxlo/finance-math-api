using FinanceMath.Application.Interfaces;
using FinanceMath.Application.Users.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Users.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<UserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var data = request.Request;

            var user = await _userRepository.GetByEmailAsync(data.Email);

            if (user == null || !_passwordHasher.Verify(data.Password, user?.PasswordHash ?? string.Empty))
                return Result<UserDto>.Fail("Invalid credentials.");

            var token = _jwtProvider.GenerateToken(user!);

            var dto = new UserDto
            {
                Id = user!.Id,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email.Value,
                CreatedAt = user.CreatedAt,
                Token = token
            };

            return Result<UserDto>.Ok(dto);
        }
    }
}
