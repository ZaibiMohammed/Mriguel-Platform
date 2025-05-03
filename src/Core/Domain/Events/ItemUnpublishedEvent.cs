using Mriguel.Domain.Common;
using Mriguel.Domain.Entities;

namespace Mriguel.Domain.Events
{
    /// <summary>
    /// Event triggered when an item is unpublished
    /// </summary>
    public class ItemUnpublishedEvent : DomainEvent
    {
        public Item Item { get; }
        
        public ItemUnpublishedEvent(Item item)
        {
            Item = item;
        }
    }
}
