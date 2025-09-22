using FinanceMath.Application.Content.Contents.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Commands
{
    public class UpdateContentCommand : IRequest<Result<ContentDto>>
    {
        public required Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Guid CreatedBy { get; set; }
        public string? MediaUrl { get; set; }
    }
}
