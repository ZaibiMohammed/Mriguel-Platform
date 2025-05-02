using System.Reflection;
using Mriguel.Application.Common.Interfaces;
using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mriguel.Infrastructure.Persistence
{
    /// <summary>
    /// The application's database context
    /// </summary>
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime,
            IDomainEventService domainEventService) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _domainEventService = domainEventService;
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Rental> Rentals => Set<Rental>();
        public DbSet<ItemImage> ItemImages => Set<ItemImage>();
        public DbSet<ItemCategory> ItemCategories => Set<ItemCategory>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ItemAvailability> ItemAvailabilities => Set<ItemAvailability>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<RentalMessage> RentalMessages => Set<RentalMessage>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<UserVerification> UserVerifications => Set<UserVerification>();
        public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId ?? "system";
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId ?? "system";
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var events = ChangeTracker.Entries<Entity>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEventsAsync(events, cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEventsAsync(DomainEvent[] events, CancellationToken cancellationToken)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.PublishAsync(@event, cancellationToken);
            }
        }
    }
}
