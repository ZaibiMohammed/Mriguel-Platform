using Mriguel.Application.Common.Exceptions;
using Mriguel.Application.Common.Interfaces;
using Mriguel.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mriguel.Application.Rentals.Commands.UpdateRentalStatus
{
    /// <summary>
    /// Command to accept a rental
    /// </summary>
    public record AcceptRentalCommand : IRequest
    {
        public Guid Id { get; init; }
    }
    
    /// <summary>
    /// Handler for accepting a rental
    /// </summary>
    public class AcceptRentalCommandHandler : IRequestHandler<AcceptRentalCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        
        public AcceptRentalCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        
        public async Task Handle(AcceptRentalCommand request, CancellationToken cancellationToken)
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
                rental.Accept();
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
