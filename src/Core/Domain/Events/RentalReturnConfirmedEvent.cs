using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a rental return is confirmed
    /// </summary>
    public class RentalReturnConfirmedEvent : DomainEvent
    {
        public Rental Rental { get; }
        
        public RentalReturnConfirmedEvent(Rental rental)
        {
            Rental = rental;
        }
    }
}
