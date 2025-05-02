using AlloVoisinClone.Application.Common.Interfaces;
using AlloVoisinClone.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlloVoisinClone.Application.Rentals.EventHandlers
{
    /// <summary>
    /// Handler for the RentalCreatedEvent
    /// </summary>
    public class RentalCreatedEventHandler : INotificationHandler<RentalCreatedEvent>
    {
        private readonly ILogger<RentalCreatedEventHandler> _logger;
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;
        
        public RentalCreatedEventHandler(
            ILogger<RentalCreatedEventHandler> logger,
            INotificationService notificationService,
            IEmailService emailService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _emailService = emailService;
        }
        
        public async Task Handle(RentalCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("AlloVoisinClone Domain Event: {DomainEvent}", notification.GetType().Name);
            
            var rental = notification.Rental;
            
            // Send notification to the owner
            await _notificationService.SendNotificationAsync(
                rental.OwnerId.ToString(),
                "New Rental Request",
                $"You have received a new rental request for {rental.Item.Title}");
                
            // Send email to the owner
            try
            {
                await _emailService.SendTemplatedEmailAsync(
                    rental.Owner.Email,
                    "RentalRequest",
                    new { ItemTitle = rental.Item.Title });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email notification for rental {RentalId}", rental.Id);
            }
        }
    }
}
