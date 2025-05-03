using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a user is deactivated
    /// </summary>
    public class UserDeactivatedEvent : DomainEvent
    {
        public User User { get; }
        
        public UserDeactivatedEvent(User user)
        {
            User = user;
        }
    }
}
