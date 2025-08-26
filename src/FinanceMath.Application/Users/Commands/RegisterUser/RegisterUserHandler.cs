using FinanceMath.Application.Interfaces;
using FinanceMath.Application.Users.Dtos;
using FinanceMath.Domain.Repositories;
using FinanceMath.Domain.Users.Entities;
using FinanceMath.Domain.Users.Enums;
using MediatR;

namespace FinanceMath.Application.Users.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var data = request.Request;

            var existingUser = await _userRepository.GetByUsernameAsync(data.Username);

            if (existingUser != null)
                return Result<UserDto>.Fail("Username already exists.");

            var hashedPassword = _passwordHasher.Hash(data.Password);

            var email = new Email(data.Email);
            var user = new User(data.Username, data.FullName, email, data.Password, UserType.Student);
            await _userRepository.AddAsync(user);

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
