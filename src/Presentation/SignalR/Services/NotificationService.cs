using Mriguel.Application.Common.Interfaces;
using Mriguel.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Mriguel.SignalR.Services
{
    /// <summary>
    /// Service for sending real-time notifications
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IApplicationDbContext _dbContext;
        
        public NotificationService(
            IHubContext<NotificationHub> notificationHubContext,
            IApplicationDbContext dbContext)
        {
            _notificationHubContext = notificationHubContext;
            _dbContext = dbContext;
        }
        
        /// <summary>
        /// Sends a notification to a specific user
        /// </summary>
        public async Task SendNotificationAsync(string userId, string title, string message, string? actionLink = null)
        {
            await _notificationHubContext.Clients
                .Group($"user-{userId}")
                .SendAsync("ReceiveNotification", new
                {
                    Title = title,
                    Message = message,
                    Timestamp = DateTime.UtcNow,
                    ActionLink = actionLink
                });
        }
        
        /// <summary>
        /// Sends a notification about a rental to the owner and renter
        /// </summary>
        public async Task SendRentalNotificationAsync(Guid rentalId, string title, string message)
        {
            var rental = await _dbContext.Rentals.FindAsync(rentalId);
            
            if (rental == null)
            {
                throw new ArgumentException($"Rental with ID {rentalId} not found");
            }
            
            // Send notification to the owner
            await SendNotificationAsync(
                rental.OwnerId.ToString(),
                title,
                message,
                $"/rentals/{rentalId}");
                
            // Send notification to the renter
            await SendNotificationAsync(
                rental.RenterId.ToString(),
                title,
                message,
                $"/rentals/{rentalId}");
        }
    }
}
