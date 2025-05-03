using Mriguel.Application.Common.Interfaces;
using Mriguel.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mriguel.Application.Rentals.EventHandlers
{
    /// <summary>
    /// Handler for the RentalAcceptedEvent
    /// </summary>
    public class RentalAcceptedEventHandler : INotificationHandler<RentalAcceptedEvent>
    {
        private readonly ILogger<RentalAcceptedEventHandler> _logger;
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;
        
        public RentalAcceptedEventHandler(
            ILogger<RentalAcceptedEventHandler> logger,
            INotificationService notificationService,
            IEmailService emailService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _emailService = emailService;
        }
        
        public async Task Handle(RentalAcceptedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("AlloVoisinClone Domain Event: {DomainEvent}", notification.GetType().Name);
            
            var rental = notification.Rental;
            
            // Send notification to the renter
            await _notificationService.SendNotificationAsync(
                rental.RenterId.ToString(),
                "Rental Request Accepted",
                $"Your rental request for {rental.Item.Title} has been accepted");
                
            // Send email to the renter
            try
            {
                await _emailService.SendTemplatedEmailAsync(
                    rental.Renter.Email,
                    "RentalConfirmation",
                    new { ItemTitle = rental.Item.Title });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email notification for rental {RentalId}", rental.Id);
            }
        }
    }
}
