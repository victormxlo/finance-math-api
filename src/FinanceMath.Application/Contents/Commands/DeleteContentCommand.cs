using MediatR;

namespace FinanceMath.Application.Contents.Commands
{
    public class DeleteContentCommand : IRequest<Result<bool>>
    {
        public required Guid Id { get; set; }
    }
}
