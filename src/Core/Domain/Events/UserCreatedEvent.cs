using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event raised when a user is created
    /// </summary>
    public class UserCreatedEvent : DomainEvent
    {
        public User User { get; }
        
        public UserCreatedEvent(User user)
        {
            User = user;
        }
    }
}
