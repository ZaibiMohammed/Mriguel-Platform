using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a rental is accepted
    /// </summary>
    public class RentalAcceptedEvent : DomainEvent
    {
        public Rental Rental { get; }
        
        public RentalAcceptedEvent(Rental rental)
        {
            Rental = rental;
        }
    }
}
