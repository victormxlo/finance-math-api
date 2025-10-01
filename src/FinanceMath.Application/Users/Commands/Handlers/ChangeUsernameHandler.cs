using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Users.Commands.Handlers
{
    public class ChangeUsernameHandler : IRequestHandler<ChangeUsernameCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;

        public ChangeUsernameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(ChangeUsernameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user is null)
                    return Result<bool>.Fail($"User not found with id: {request.Id}.");

                user.UpdateUsername(request.NewUsername);

                await _userRepository.UpdateAsync(user);

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Failed to change username: {ex.Message}.");
            }
        }
    }
}
