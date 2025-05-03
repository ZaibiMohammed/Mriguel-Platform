using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a user is banned
    /// </summary>
    public class UserBannedEvent : DomainEvent
    {
        public User User { get; }
        public string Reason { get; }
        
        public UserBannedEvent(User user, string reason)
        {
            User = user;
            Reason = reason;
        }
    }
}
