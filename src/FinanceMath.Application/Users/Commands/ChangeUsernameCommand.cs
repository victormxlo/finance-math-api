using MediatR;

namespace FinanceMath.Application.Users.Commands
{
    public class ChangeUsernameCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public required string NewUsername { get; set; }
    }
}
