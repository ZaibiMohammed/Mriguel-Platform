using System;
using Mriguel.Domain.Common;

namespace Mriguel.Domain.Entities
{
    /// <summary>
    /// Represents a review for an item or user
    /// </summary>
    public class Review : AuditableEntity
    {
        private Review() { } // Required by EF Core
        
        public Review(string comment, int rating, User reviewer, Rental rental)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5");
                
            Comment = comment;
            Rating = rating;
            ReviewerId = reviewer?.Id ?? throw new ArgumentNullException(nameof(reviewer));
            Reviewer = reviewer;
            RentalId = rental?.Id ?? throw new ArgumentNullException(nameof(rental));
            Rental = rental;
            
            // Determine if this is an item review or a user review based on the reviewer
            if (reviewer.Id == rental.OwnerId)
            {
                // Owner is reviewing the renter
                ReviewedUserId = rental.RenterId;
                ReviewedUser = rental.Renter;
            }
            else if (reviewer.Id == rental.RenterId)
            {
                // Renter is reviewing the item and possibly the owner
                ItemId = rental.ItemId;
                Item = rental.Item;
                
                // Optionally also review the owner
                ReviewedUserId = rental.OwnerId;
                ReviewedUser = rental.Owner;
            }
            else
            {
                throw new ArgumentException("Reviewer must be either the owner or the renter of the rental");
            }
        }
        
        public string Comment { get; private set; }
        public int Rating { get; private set; }
        public Guid ReviewerId { get; private set; }
        public User Reviewer { get; private set; }
        public Guid? ReviewedUserId { get; private set; }
        public User ReviewedUser { get; private set; }
        public Guid? ItemId { get; private set; }
        public Item Item { get; private set; }
        public Guid RentalId { get; private set; }
        public Rental Rental { get; private set; }
        
        public void UpdateComment(string comment)
        {
            Comment = comment;
        }
        
        public void UpdateRating(int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5");
                
            Rating = rating;
        }
    }
}
