using MediatR;

namespace FinanceMath.Application.Users.Commands
{
    public class ChangeUserPasswordCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
