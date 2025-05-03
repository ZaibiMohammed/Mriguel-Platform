using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a rental is declined
    /// </summary>
    public class RentalDeclinedEvent : DomainEvent
    {
        public Rental Rental { get; }
        
        public RentalDeclinedEvent(Rental rental)
        {
            Rental = rental;
        }
    }
}
