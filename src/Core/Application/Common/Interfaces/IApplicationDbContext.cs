using AlloVoisinClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlloVoisinClone.Application.Common.Interfaces
{
    /// <summary>
    /// Interface for the application database context
    /// </summary>
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Item> Items { get; }
        DbSet<Rental> Rentals { get; }
        DbSet<ItemImage> ItemImages { get; }
        DbSet<ItemCategory> ItemCategories { get; }
        DbSet<Category> Categories { get; }
        DbSet<ItemAvailability> ItemAvailabilities { get; }
        DbSet<Review> Reviews { get; }
        DbSet<RentalMessage> RentalMessages { get; }
        DbSet<Payment> Payments { get; }
        DbSet<Address> Addresses { get; }
        DbSet<UserVerification> UserVerifications { get; }
        DbSet<PaymentMethod> PaymentMethods { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
