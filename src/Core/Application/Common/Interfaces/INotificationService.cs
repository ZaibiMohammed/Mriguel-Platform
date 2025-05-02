namespace AlloVoisinClone.Application.Common.Interfaces
{
    /// <summary>
    /// Interface for the notification service
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Sends a notification to a specific user
        /// </summary>
        Task SendNotificationAsync(string userId, string title, string message, string? actionLink = null);
        
        /// <summary>
        /// Sends a notification about a rental to the owner
        /// </summary>
        Task SendRentalNotificationAsync(Guid rentalId, string title, string message);
    }
}
