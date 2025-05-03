using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a rental is paid
    /// </summary>
    public class RentalPaidEvent : DomainEvent
    {
        public Rental Rental { get; }
        
        public RentalPaidEvent(Rental rental)
        {
            Rental = rental;
        }
    }
}
