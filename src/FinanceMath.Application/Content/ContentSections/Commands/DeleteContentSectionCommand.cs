using MediatR;

namespace FinanceMath.Application.Content.ContentSections.Commands
{
    public class DeleteContentSectionCommand : IRequest<Result<bool>>
    {
        public Guid ContentId { get; set; }
        public Guid ContentSectionId { get; set; }
    }
}
