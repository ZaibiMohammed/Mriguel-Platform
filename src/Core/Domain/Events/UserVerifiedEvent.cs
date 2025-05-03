using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a user verification is added
    /// </summary>
    public class UserVerifiedEvent : DomainEvent
    {
        public User User { get; }
        public string VerificationType { get; }
        
        public UserVerifiedEvent(User user, string verificationType)
        {
            User = user;
            VerificationType = verificationType;
        }
    }
}
