using Mriguel.Application.Common.Interfaces;
using Mriguel.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mriguel.Infrastructure.Services
{
    /// <summary>
    /// Service for publishing domain events
    /// </summary>
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;
        private readonly IPublisher _mediator;

        public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
