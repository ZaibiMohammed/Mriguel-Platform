using AlloVoisinClone.Domain.Common;

namespace AlloVoisinClone.Application.Common.Interfaces
{
    /// <summary>
    /// Interface for domain event service
    /// </summary>
    public interface IDomainEventService
    {
        /// <summary>
        /// Publishes a domain event
        /// </summary>
        Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
