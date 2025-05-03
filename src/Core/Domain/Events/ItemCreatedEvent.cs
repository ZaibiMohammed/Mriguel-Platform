using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when an item is created
    /// </summary>
    public class ItemCreatedEvent : DomainEvent
    {
        public Item Item { get; }
        
        public ItemCreatedEvent(Item item)
        {
            Item = item;
        }
    }
}
