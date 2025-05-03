using System;
using Mriguel.Domain.Common;

namespace Mriguel.Domain.Entities
{
    /// <summary>
    /// Represents a message exchanged between the renter and owner during a rental
    /// </summary>
    public class RentalMessage : AuditableEntity
    {
        private RentalMessage() { } // Required by EF Core
        
        public RentalMessage(string content, User sender, Rental rental)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            SenderId = sender?.Id ?? throw new ArgumentNullException(nameof(sender));
            Sender = sender;
            RentalId = rental?.Id ?? throw new ArgumentNullException(nameof(rental));
            Rental = rental;
            IsRead = false;
        }
        
        public string Content { get; private set; }
        public Guid SenderId { get; private set; }
        public User Sender { get; private set; }
        public Guid RentalId { get; private set; }
        public Rental Rental { get; private set; }
        public bool IsRead { get; private set; }
        
        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}
