using Mriguel.Domain.Entities;
using Mriguel.Domain.Enums;
using Mriguel.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mriguel.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for Item entity
    /// </summary>
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(i => i.Title)
                .HasMaxLength(200)
                .IsRequired();
                
            builder.Property(i => i.Description)
                .HasMaxLength(2000)
                .IsRequired();
                
            builder.OwnsOne(i => i.DailyPrice, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                    .HasColumnName("DailyPriceAmount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                    
                moneyBuilder.Property(m => m.Currency)
                    .HasColumnName("DailyPriceCurrency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
            
            builder.OwnsOne(i => i.SecurityDeposit, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                    .HasColumnName("SecurityDepositAmount")
                    .HasColumnType("decimal(18,2)");
                    
                moneyBuilder.Property(m => m.Currency)
                    .HasColumnName("SecurityDepositCurrency")
                    .HasMaxLength(3);
            });
            
            builder.OwnsOne(i => i.Location, locationBuilder =>
            {
                locationBuilder.Property(l => l.Latitude)
                    .HasColumnName("Latitude")
                    .IsRequired();
                    
                locationBuilder.Property(l => l.Longitude)
                    .HasColumnName("Longitude")
                    .IsRequired();
                    
                locationBuilder.Property(l => l.Address)
                    .HasColumnName("Address")
                    .HasMaxLength(500)
                    .IsRequired();
            });
            
            builder.Property(i => i.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (ItemStatus)Enum.Parse(typeof(ItemStatus), v))
                .HasMaxLength(50)
                .IsRequired();
                
            builder.HasMany(i => i.Images)
                .WithOne()
                .HasForeignKey("ItemId")
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(i => i.Categories)
                .WithOne()
                .HasForeignKey("ItemId")
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(i => i.Availabilities)
                .WithOne()
                .HasForeignKey("ItemId")
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(i => i.Rentals)
                .WithOne(r => r.Item)
                .HasForeignKey(r => r.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(i => i.Reviews)
                .WithOne()
                .HasForeignKey("ItemId")
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasIndex(i => i.OwnerId);
            builder.HasIndex(i => i.Status);
        }
    }
}
