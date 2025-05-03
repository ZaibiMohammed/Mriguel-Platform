using Mriguel.Application.Common.Interfaces;
using Mriguel.Domain.Entities;
using Mriguel.Domain.ValueObjects;
using MediatR;

namespace Mriguel.Application.Items.Commands.CreateItem
{
    /// <summary>
    /// Command to create a new item
    /// </summary>
    public record CreateItemCommand : IRequest<Guid>
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal DailyPrice { get; init; }
        public string Currency { get; init; } = "USD";
        public decimal? SecurityDeposit { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string Address { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string PostalCode { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
    }
    
    /// <summary>
    /// Handler for creating a new item
    /// </summary>
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        
        public CreateItemCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        
        public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_currentUserService.UserId!);
            var user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);
            
            if (user == null)
            {
                throw new Application.Common.Exceptions.NotFoundException(nameof(User), userId);
            }
            
            var dailyPrice = new Money(request.DailyPrice, request.Currency);
            
            // Create the item first
            var location = new Location(request.Address, request.City, request.PostalCode, request.Country, request.Latitude, request.Longitude);
            var item = new Item(request.Title, request.Description, dailyPrice, user, location);
            
            // If security deposit is provided, update the item with it
            if (request.SecurityDeposit.HasValue)
            {
                var securityDeposit = new Money(request.SecurityDeposit.Value, request.Currency);
                item.Update(request.Title, request.Description, dailyPrice, securityDeposit, location);
            }
            
            await _context.Items.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return item.Id;
        }
    }
}
