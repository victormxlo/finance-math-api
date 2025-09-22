using FinanceMath.Application.Content.Contents.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Commands
{
    public class CreateContentCommand : IRequest<Result<ContentDto>>
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Guid CreatedBy { get; set; }
        public string? MediaUrl { get; set; }
    }
}
