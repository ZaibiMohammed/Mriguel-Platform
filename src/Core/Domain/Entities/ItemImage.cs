using System;
using Mriguel.Domain.Common;

namespace Mriguel.Domain.Entities
{
    /// <summary>
    /// Represents an image associated with an item
    /// </summary>
    public class ItemImage : AuditableEntity
    {
        private ItemImage() { } // Required by EF Core
        
        public ItemImage(string url, string thumbnailUrl, Item item)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            ThumbnailUrl = thumbnailUrl ?? throw new ArgumentNullException(nameof(thumbnailUrl));
            ItemId = item?.Id ?? throw new ArgumentNullException(nameof(item));
            Item = item;
        }
        
        public string Url { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public bool IsMain { get; private set; }
        public bool IsCoverImage { get; private set; }
        public int DisplayOrder { get; private set; }
        public Guid ItemId { get; private set; }
        public Item Item { get; private set; }
        
        public void SetAsMain()
        {
            IsMain = true;
        }
        
        public void SetAsCoverImage(bool isCoverImage = true)
        {
            IsCoverImage = isCoverImage;
        }
        
        public void SetDisplayOrder(int order)
        {
            if (order < 0)
                throw new ArgumentException("Display order cannot be negative", nameof(order));
                
            DisplayOrder = order;
        }
    }
}
