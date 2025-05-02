using AlloVoisinClone.Domain.Common;
using AlloVoisinClone.Domain.Entities;

namespace AlloVoisinClone.Domain.Events
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
