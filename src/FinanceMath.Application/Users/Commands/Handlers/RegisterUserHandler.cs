using FinanceMath.Application.Interfaces;
using FinanceMath.Application.Users.Dtos;
using FinanceMath.Domain.Repositories;
using FinanceMath.Domain.Users.Entities;
using FinanceMath.Domain.Users.Enums;
using MediatR;

namespace FinanceMath.Application.Users.Commands.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public RegisterUserHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = request.Request;

                var existingUser = await _userRepository.GetByUsernameAsync(data.Username);

                if (existingUser != null)
                    return Result<UserDto>.Fail("Username already exists.");

                var hashedPassword = _passwordHasher.Hash(data.Password);

                var type = data.Type == (int)UserType.Admin ?
                    UserType.Admin : UserType.Student;

                var email = new Email(data.Email);
                var user = new User(data.Username, data.FullName, email, hashedPassword, type);
                await _userRepository.AddAsync(user);

                var token = _jwtProvider.GenerateToken(user);

                var dto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    FullName = user.FullName,
                    Email = user.Email.Value,
                    Type = Enum.GetName<UserType>(user.Type)!,
                    CreatedAt = user.CreatedAt,
                    Token = token
                };

                return Result<UserDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<UserDto>.Fail($"Failed to register user: {ex.Message}.");
            }
        }
    }
}
