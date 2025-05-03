using System;
using Mriguel.Domain.Common;

namespace Mriguel.Domain.Entities
{
    /// <summary>
    /// Represents a relationship between an item and a category
    /// </summary>
    public class ItemCategory : Entity
    {
        private ItemCategory() { } // Required by EF Core
        
        public ItemCategory(Item item, Category category)
        {
            ItemId = item?.Id ?? throw new ArgumentNullException(nameof(item));
            Item = item;
            CategoryId = category?.Id ?? throw new ArgumentNullException(nameof(category));
            Category = category;
        }
        
        public Guid ItemId { get; private set; }
        public Item Item { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }
    }
}
