using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a rental pickup is confirmed
    /// </summary>
    public class RentalPickupConfirmedEvent : DomainEvent
    {
        public Rental Rental { get; }
        
        public RentalPickupConfirmedEvent(Rental rental)
        {
            Rental = rental;
        }
    }
}
