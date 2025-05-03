using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a rental is cancelled
    /// </summary>
    public class RentalCancelledEvent : DomainEvent
    {
        public Rental Rental { get; }
        
        public RentalCancelledEvent(Rental rental)
        {
            Rental = rental;
        }
    }
}
