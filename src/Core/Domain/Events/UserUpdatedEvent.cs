using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a user profile is updated
    /// </summary>
    public class UserUpdatedEvent : DomainEvent
    {
        public User User { get; }
        
        public UserUpdatedEvent(User user)
        {
            User = user;
        }
    }
}
