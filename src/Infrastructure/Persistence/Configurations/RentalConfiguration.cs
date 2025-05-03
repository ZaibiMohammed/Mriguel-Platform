using Mriguel.Domain.Entities;
using Mriguel.Domain.Enums;
using Mriguel.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mriguel.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for Rental entity
    /// </summary>
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.Property(r => r.StartDate)
                .IsRequired();
                
            builder.Property(r => r.EndDate)
                .IsRequired();
                
            builder.Property(r => r.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (RentalStatus)Enum.Parse(typeof(RentalStatus), v))
                .HasMaxLength(50)
                .IsRequired();
                
            builder.OwnsOne(r => r.TotalPrice, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                    .HasColumnName("TotalPriceAmount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                    
                moneyBuilder.Property(m => m.Currency)
                    .HasColumnName("TotalPriceCurrency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
            
            builder.OwnsOne(r => r.SecurityDeposit, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                    .HasColumnName("SecurityDepositAmount")
                    .HasColumnType("decimal(18,2)");
                    
                moneyBuilder.Property(m => m.Currency)
                    .HasColumnName("SecurityDepositCurrency")
                    .HasMaxLength(3);
            });
            
            builder.Property(r => r.CancellationReason)
                .HasMaxLength(1000);
                
            builder.Property(r => r.DeclineReason)
                .HasMaxLength(1000);
                
            builder.HasMany(r => r.Messages)
                .WithOne()
                .HasForeignKey("RentalId")
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(r => r.Payments)
                .WithOne()
                .HasForeignKey("RentalId")
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(r => r.Reviews)
                .WithOne(r => r.Rental)
                .HasForeignKey(r => r.RentalId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasIndex(r => r.ItemId);
            builder.HasIndex(r => r.RenterId);
            builder.HasIndex(r => r.OwnerId);
            builder.HasIndex(r => r.Status);
        }
    }
}
