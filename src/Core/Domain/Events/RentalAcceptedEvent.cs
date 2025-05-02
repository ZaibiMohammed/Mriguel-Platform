using AlloVoisinClone.Domain.Common;
using AlloVoisinClone.Domain.Entities;

namespace AlloVoisinClone.Domain.Events
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
