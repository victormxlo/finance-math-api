using FinanceMath.Application.Interfaces;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Users.Commands.Handlers
{
    public class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPasswordCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ChangeUserPasswordHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<bool>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user == null || !_passwordHasher.Verify(request.CurrentPassword, user?.PasswordHash ?? string.Empty))
                    return Result<bool>.Fail("Invalid credentials.");

                var hashedPassword = _passwordHasher.Hash(request.NewPassword);
                user!.ChangePassword(hashedPassword);

                await _userRepository.UpdateAsync(user);

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Failed to change user password: {ex.Message}.");
            }
        }
    }
}
