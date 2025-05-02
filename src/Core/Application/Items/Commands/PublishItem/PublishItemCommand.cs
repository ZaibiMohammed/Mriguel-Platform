using AlloVoisinClone.Application.Common.Exceptions;
using AlloVoisinClone.Application.Common.Interfaces;
using AlloVoisinClone.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlloVoisinClone.Application.Items.Commands.PublishItem
{
    /// <summary>
    /// Command to publish an item
    /// </summary>
    public record PublishItemCommand : IRequest
    {
        public Guid Id { get; init; }
    }
    
    /// <summary>
    /// Handler for publishing an item
    /// </summary>
    public class PublishItemCommandHandler : IRequestHandler<PublishItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        
        public PublishItemCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        
        public async Task Handle(PublishItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items
                .Include(i => i.Images)
                .Include(i => i.Categories)
                .Include(i => i.Availabilities)
                .Where(i => i.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);
                
            if (item == null)
            {
                throw new NotFoundException(nameof(Item), request.Id);
            }
            
            var userId = Guid.Parse(_currentUserService.UserId!);
            
            if (item.OwnerId != userId)
            {
                throw new ForbiddenAccessException();
            }
            
            try
            {
                item.Publish();
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Domain.Exceptions.DomainException ex)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Error", new[] { ex.Message } }
                });
            }
        }
    }
}
