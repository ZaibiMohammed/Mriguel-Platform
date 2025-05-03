using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a message is added to a rental
    /// </summary>
    public class RentalMessageAddedEvent : DomainEvent
    {
        public RentalMessage Message { get; }
        public Rental Rental { get; }
        
        public RentalMessageAddedEvent(RentalMessage message, Rental rental)
        {
            Message = message;
            Rental = rental;
        }
    }
}
