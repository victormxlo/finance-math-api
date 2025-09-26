using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Application.Interfaces;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Commands.Handlers
{
    public class CompleteContentHandler : IRequestHandler<CompleteContentCommand, Result<CompleteContentResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IGamificationService _gamificationService;

        public CompleteContentHandler(IUserRepository userRepository, IContentRepository contentRepository, IGamificationService gamificationService)
        {
            _userRepository = userRepository;
            _contentRepository = contentRepository;
            _gamificationService = gamificationService;
        }

        public async Task<Result<CompleteContentResponseDto>> Handle(CompleteContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user is null)
                    return Result<CompleteContentResponseDto>.Fail($"User not found with id: {request.UserId}.");

                var content = await _contentRepository.GetByIdAsync(request.ContentId);

                if (content is null)
                    return Result<CompleteContentResponseDto>.Fail($"Content not found with id: {request.ContentId}.");

                var response = await _gamificationService.CompleteContentAsync(user.Id, content.Id);

                return Result<CompleteContentResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                return Result<CompleteContentResponseDto>.Fail($"Failed to complete content: {ex.Message}.");
            }
        }
    }
}
