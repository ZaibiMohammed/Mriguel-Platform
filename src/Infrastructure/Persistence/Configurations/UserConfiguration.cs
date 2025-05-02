using AlloVoisinClone.Domain.Entities;
using AlloVoisinClone.Domain.Enums;
using AlloVoisinClone.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlloVoisinClone.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for User entity
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email)
                .HasMaxLength(256)
                .IsRequired();
                
            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();
                
            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();
                
            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20);
                
            builder.Property(u => u.Biography)
                .HasMaxLength(1000);
                
            builder.Property(u => u.ProfilePictureUrl)
                .HasMaxLength(1000);
                
            builder.Property(u => u.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserStatus)Enum.Parse(typeof(UserStatus), v))
                .HasMaxLength(50)
                .IsRequired();
                
            builder.Property(u => u.IdentityUserId)
                .HasMaxLength(450)
                .IsRequired();
                
            builder.HasMany(u => u.Items)
                .WithOne(i => i.Owner)
                .HasForeignKey(i => i.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(u => u.RentalsAsRenter)
                .WithOne(r => r.Renter)
                .HasForeignKey(r => r.RenterId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(u => u.RentalsAsOwner)
                .WithOne(r => r.Owner)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(u => u.ReviewsGiven)
                .WithOne(r => r.Reviewer)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(u => u.ReviewsReceived)
                .WithOne(r => r.Reviewee)
                .HasForeignKey(r => r.RevieweeId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(u => u.Addresses)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(u => u.Verifications)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(u => u.PaymentMethods)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasIndex(u => u.Email)
                .IsUnique();
                
            builder.HasIndex(u => u.IdentityUserId)
                .IsUnique();
        }
    }
}
