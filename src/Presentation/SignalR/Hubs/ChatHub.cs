using System.Security.Claims;
using Mriguel.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Mriguel.SignalR.Hubs
{
    /// <summary>
    /// SignalR hub for real-time chat
    /// </summary>
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IApplicationDbContext _dbContext;
        
        public ChatHub(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        /// <summary>
        /// Join a rental chat room
        /// </summary>
        public async Task JoinRentalChat(Guid rentalId)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId))
            {
                throw new HubException("User not authenticated");
            }
            
            var rental = await _dbContext.Rentals.FindAsync(rentalId);
            
            if (rental == null)
            {
                throw new HubException("Rental not found");
            }
            
            // Check if the user is either the renter or the owner
            if (rental.RenterId.ToString() != userId && rental.OwnerId.ToString() != userId)
            {
                throw new HubException("User not authorized to join this chat");
            }
            
            // Join the group
            await Groups.AddToGroupAsync(Context.ConnectionId, $"rental-{rentalId}");
        }
        
        /// <summary>
        /// Leave a rental chat room
        /// </summary>
        public async Task LeaveRentalChat(Guid rentalId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"rental-{rentalId}");
        }
        
        /// <summary>
        /// Send a message to a rental chat room
        /// </summary>
        public async Task SendMessage(Guid rentalId, string message)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId))
            {
                throw new HubException("User not authenticated");
            }
            
            var rental = await _dbContext.Rentals
                .FindAsync(rentalId);
                
            if (rental == null)
            {
                throw new HubException("Rental not found");
            }
            
            // Check if the user is either the renter or the owner
            if (rental.RenterId.ToString() != userId && rental.OwnerId.ToString() != userId)
            {
                throw new HubException("User not authorized to send messages in this chat");
            }
            
            // Get the user
            var user = await _dbContext.Users.FindAsync(Guid.Parse(userId));
            
            if (user == null)
            {
                throw new HubException("User not found");
            }
            
            // Add the message to the rental
            rental.AddMessage(user, message);
            await _dbContext.SaveChangesAsync();
            
            // Send the message to all clients in the group
            await Clients.Group($"rental-{rentalId}").SendAsync("ReceiveMessage", new
            {
                RentalId = rentalId,
                SenderId = userId,
                SenderName = $"{user.FirstName} {user.LastName}",
                SenderProfilePictureUrl = user.ProfilePictureUrl,
                Message = message,
                Timestamp = DateTime.UtcNow
            });
        }
        
        /// <summary>
        /// Notify when a user is typing
        /// </summary>
        public async Task NotifyTyping(Guid rentalId)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId))
            {
                throw new HubException("User not authenticated");
            }
            
            // Send typing notification to all clients in the group except the sender
            await Clients.OthersInGroup($"rental-{rentalId}").SendAsync("UserTyping", new
            {
                RentalId = rentalId,
                UserId = userId
            });
        }
    }
}
