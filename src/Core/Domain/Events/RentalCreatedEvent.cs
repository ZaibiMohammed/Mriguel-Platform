using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a rental is created
    /// </summary>
    public class RentalCreatedEvent : DomainEvent
    {
        public Rental Rental { get; }
        
        public RentalCreatedEvent(Rental rental)
        {
            Rental = rental;
        }
    }
}
