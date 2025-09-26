using FinanceMath.Application.Content.Contents.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Commands
{
    public class CompleteContentCommand : IRequest<Result<CompleteContentResponseDto>>
    {
        public Guid ContentId { get; set; }
        public Guid UserId { get; set; }
    }
}
