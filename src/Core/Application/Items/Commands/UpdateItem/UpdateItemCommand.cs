using AlloVoisinClone.Application.Common.Exceptions;
using AlloVoisinClone.Application.Common.Interfaces;
using AlloVoisinClone.Domain.Entities;
using AlloVoisinClone.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlloVoisinClone.Application.Items.Commands.UpdateItem
{
    /// <summary>
    /// Command to update an item
    /// </summary>
    public record UpdateItemCommand : IRequest
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal DailyPrice { get; init; }
        public string Currency { get; init; } = "USD";
        public decimal? SecurityDeposit { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string Address { get; init; } = string.Empty;
    }
    
    /// <summary>
    /// Handler for updating an item
    /// </summary>
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        
        public UpdateItemCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        
        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items
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
            
            var dailyPrice = new Money(request.DailyPrice, request.Currency);
            
            Money? securityDeposit = null;
            if (request.SecurityDeposit.HasValue)
            {
                securityDeposit = new Money(request.SecurityDeposit.Value, request.Currency);
            }
            
            var location = new Location(request.Latitude, request.Longitude, request.Address);
            
            item.Update(request.Title, request.Description, dailyPrice, securityDeposit, location);
            
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
