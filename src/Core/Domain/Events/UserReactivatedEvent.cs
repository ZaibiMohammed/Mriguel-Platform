using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a user is reactivated
    /// </summary>
    public class UserReactivatedEvent : DomainEvent
    {
        public User User { get; }
        
        public UserReactivatedEvent(User user)
        {
            User = user;
        }
    }
}
