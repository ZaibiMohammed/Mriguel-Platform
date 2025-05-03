using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when a review is added to a rental
    /// </summary>
    public class RentalReviewAddedEvent : DomainEvent
    {
        public Review Review { get; }
        public Rental Rental { get; }
        
        public RentalReviewAddedEvent(Review review, Rental rental)
        {
            Review = review;
            Rental = rental;
        }
    }
}
