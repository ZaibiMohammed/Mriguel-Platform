using AlloVoisinClone.Application.Common.Exceptions;
using AlloVoisinClone.Application.Common.Interfaces;
using AlloVoisinClone.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlloVoisinClone.Application.Rentals.Commands.UpdateRentalStatus
{
    /// <summary>
    /// Command to decline a rental
    /// </summary>
    public record DeclineRentalCommand : IRequest
    {
        public Guid Id { get; init; }
        public string Reason { get; init; } = string.Empty;
    }
    
    /// <summary>
    /// Handler for declining a rental
    /// </summary>
    public class DeclineRentalCommandHandler : IRequestHandler<DeclineRentalCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        
        public DeclineRentalCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        
        public async Task Handle(DeclineRentalCommand request, CancellationToken cancellationToken)
        {
            var rental = await _context.Rentals
                .Include(r => r.Item)
                .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
                
            if (rental == null)
            {
                throw new NotFoundException(nameof(Rental), request.Id);
            }
            
            if (rental.Item.OwnerId.ToString() != _currentUserService.UserId)
            {
                throw new ForbiddenAccessException();
            }
            
            try
            {
                rental.Decline(request.Reason);
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
