using AlloVoisinClone.Application.Common.Exceptions;
using AlloVoisinClone.Application.Common.Interfaces;
using AlloVoisinClone.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlloVoisinClone.Application.Rentals.Commands.CreateRental
{
    /// <summary>
    /// Command to create a new rental
    /// </summary>
    public record CreateRentalCommand : IRequest<Guid>
    {
        public Guid ItemId { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public string Message { get; init; } = string.Empty;
    }
    
    /// <summary>
    /// Handler for creating a new rental
    /// </summary>
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        
        public CreateRentalCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        
        public async Task<Guid> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_currentUserService.UserId!);
            
            var item = await _context.Items
                .Include(i => i.Owner)
                .Include(i => i.Availabilities)
                .Include(i => i.Rentals)
                .Where(i => i.Id == request.ItemId)
                .SingleOrDefaultAsync(cancellationToken);
                
            if (item == null)
            {
                throw new NotFoundException(nameof(Item), request.ItemId);
            }
            
            var renter = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);
            
            if (renter == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            
            try
            {
                var rental = new Rental(
                    item,
                    renter,
                    request.StartDate,
                    request.EndDate,
                    request.Message);
                    
                await _context.Rentals.AddAsync(rental, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                
                return rental.Id;
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
